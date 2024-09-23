using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using IPMS.Services.WorkFlow;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using IPMS.Domain;
using System.Linq;
using System.Globalization;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                      ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BerthPreSchedulingService : ServiceBase, IBerthPreSchedulingService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IBerthPreSchedulingRepository _berthpreschedulingRepository;
        private IBerthRepository _berthRepository;
        private IQuayRepository _quayRepository;
        //private IPortConfigurationRepository _portConfigurationRepository;
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entityRepository;
        //private IShiftRepository _shiftRepository;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        private IBerthPlanningRepository _berthPlanningRepository;
        private IServiceRequestRepository _servicerequestrepository;
        private const string _entityCode = EntityCodes.Berth_PreScheduling;

        public BerthPreSchedulingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _quayRepository = new QuayRepository(_unitOfWork);
            _servicerequestrepository = new ServiceRequestRepository(_unitOfWork);
            _berthpreschedulingRepository = new BerthPreSchedulingRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            // _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            //_shiftRepository = new ShiftRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _berthPlanningRepository = new BerthPlanningRepository(_unitOfWork);

        }
        public BerthPreSchedulingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _quayRepository = new QuayRepository(_unitOfWork);
            _berthpreschedulingRepository = new BerthPreSchedulingRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            // _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            _servicerequestrepository = new ServiceRequestRepository(_unitOfWork);
            //_shiftRepository = new ShiftRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _berthPlanningRepository = new BerthPlanningRepository(_unitOfWork);
        }


        /// <summary>
        /// To Get BerthPreScheduling Reference data 
        /// </summary>
        /// <returns></returns>
        public BerthPreSchedulingReferenceVO GetBerthPreSchedulingReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                BerthPreSchedulingReferenceVO _berthpreschedulingreferenceVO = new BerthPreSchedulingReferenceVO();
                _berthpreschedulingreferenceVO.Agents = _berthpreschedulingRepository.GetAllAgents(_PortCode);
                _berthpreschedulingreferenceVO.CargoType = _subcategoryRepository.GetCargoTypes();
                _berthpreschedulingreferenceVO.ReasonForVisit = _subcategoryRepository.GetReasonsForVisit();
                _berthpreschedulingreferenceVO.VesselType = _subcategoryRepository.GetVesselTypes();
                //_berthpreschedulingreferenceVO.MovementStatus = _subcategoryRepository.GetMovementsStatus();
                _berthpreschedulingreferenceVO.MovementStatus = _subcategoryRepository.GetMovementsStatus().FindAll(a => a.SubCatCode != MovementStatus.UNSCHEDULED);
                return _berthpreschedulingreferenceVO;
            });
        }



        public List<VCMData> GetVesselCallMovements(string agentId, string eta, string etd, string vesselType, string reasonForVisit, string cargoType, string movementStatus)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<VCMData> _VCMList = new List<VCMData>();
                _VCMList = _berthpreschedulingRepository.GetVCMList(_PortCode, _UserId, _UserType, agentId, eta, etd, vesselType, reasonForVisit, cargoType, movementStatus);
                return _VCMList;
            });

        }

        /// <summary>
        /// To Get Reference Data For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        public BerthPlanningTableReferenceVO GetBerthPlanningTableReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                BerthPlanningTableReferenceVO _berthplanningtablereferenceVO = new BerthPlanningTableReferenceVO();
                _berthplanningtablereferenceVO.Berths = _berthRepository.GetBerths(_PortCode);
                _berthplanningtablereferenceVO.Quays = _quayRepository.QuayPortDetails(_PortCode);
                _berthplanningtablereferenceVO.VesselStatuses = _subcategoryRepository.GetMovementsStatus().FindAll(a => a.SubCatCode != MovementStatus.UNSCHEDULED && a.SubCatCode != MovementStatus.PENDING);


                return _berthplanningtablereferenceVO;

            });
        }


        /// <summary>
        /// To Get Suitable Berths for the VCN
        /// </summary>
        /// <returns></returns>
        public List<SuitableBerthVO> GetSuitableBerths(string vcn, string etb, string etub, string vesselCallMovementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                List<SuitableBerthVO> _berthlist = new List<SuitableBerthVO>();
                _berthlist = _berthpreschedulingRepository.GetSuitableBerths(vcn, _PortCode, _UserId, _UserType, etb, etub, vesselCallMovementId);
                return _berthlist;

            });
        }



        /// <summary>
        /// To Get VCM Details For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        public List<VCMTableData> GetVesselCallMovementsTable(string quayCode, string berthCode, string vesselStatus, string eta, string toDate)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                List<VCMTableData> _VCMTableList = new List<VCMTableData>();

                _VCMTableList = _berthpreschedulingRepository.GetVCMTableList(_PortCode, _UserId, _UserType, quayCode, berthCode, vesselStatus, eta, toDate);

                return _VCMTableList;
            });

        }

        /// <summary>
        /// To Get Entity Details Based on EntitiyCode For Notifications
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        public Entity GetEntities(string entityCode)
        {
            return _entityRepository.GetEntitiesNotification(entityCode);
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int userId)
        {
            return _berthpreschedulingRepository.GetUserDetails(_UserId);
        }

        /// <summary>
        /// To Modify Berth Pre-Scheduling
        /// </summary>
        /// <param name="craftdata"></param>
        /// <returns></returns>
        public string ModifyBerthPreScheduling(BerthPreSchedulingVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                string checkavailablility = "false";

                var berthslist = new List<BerthsData>();

                if (data.IsScheduleStatus == true)
                {
                    var tobollardfrommeter = data.FromBollardMeter + data.LengthOverallInM;
                    var toBollard = new BollardData();
                    var berthswithbollards = _berthPlanningRepository.GetQuayBerthsBollard(data.PortCode).FindAll(q => q.QuayCode == data.QuayCode).ToList();

                    foreach (QuayBerthBollardData berthbollarddata in berthswithbollards)
                    {
                        var berths = berthbollarddata.Berths;
                        foreach (BerthsData berth in berths)
                        {
                            var bollards = berth.Bollards;
                            foreach (BollardData bollard in bollards)
                            {
                                if (bollard.FromMeter < tobollardfrommeter && tobollardfrommeter <= bollard.ToMeter)
                                {
                                    toBollard = bollard;
                                }
                            }
                        }
                    }

                    if (toBollard.BollardCode != null)
                    {
                        checkavailablility = CheckBerthAvailability(data.PortCode, data.VCN, data.VesselCallMovementID.ToString(CultureInfo.InvariantCulture), data.QuayCode, data.SheduledBerth, data.FromBollardMeter.ToString(CultureInfo.InvariantCulture), toBollard.BerthCode, toBollard.FromMeter.ToString(CultureInfo.InvariantCulture), data.ETB, data.ETUB);

                        if (checkavailablility == "true")
                        {
                            var suitableberths = _berthpreschedulingRepository.GetSuitableBerths(data.VCN, data.PortCode, _UserId, _UserType, data.ETB, data.ETUB, data.VesselCallMovementID.ToString());

                            //var fromberthcount = suitableberths.Find(p => p.PortCode == vesselcalldata.PortCode && p.QuayCode == vesselcalldata.QuayCode && p.BerthCode == vesselcalldata.SheduledBerth);
                            //var toberthcount = suitableberths.Find(p => p.PortCode == toBollard.PortCode && p.QuayCode == toBollard.QuayCode && p.BerthCode == toBollard.BerthCode);

                            foreach (QuayBerthBollardData berthbollarddata in berthswithbollards)
                            {
                                var berths = berthbollarddata.Berths;
                                foreach (BerthsData berth in berths)
                                {
                                    var bollards = berth.Bollards;
                                    foreach (BollardData bollard in bollards)
                                    {
                                        if (bollard.FromMeter >= data.FromBollardMeter && bollard.FromMeter <= toBollard.FromMeter)
                                        {
                                            var count = berthslist.Find(a => a.BerthCode == berth.BerthCode);
                                            if (count == null)
                                            {
                                                berthslist.Add(berth);
                                            }

                                            if (bollard.Continous == "N")
                                            {
                                                checkavailablility = "Bollards are discontinous.";
                                            }
                                        }
                                    }
                                }
                            }


                            foreach (BerthsData berth in berthslist)
                            {
                                var chkberth = suitableberths.Find(b => b.BerthCode == berth.BerthCode);

                                if (chkberth == null)
                                {
                                    checkavailablility = "Berth is not suitable.";
                                }
                            }

                            if (checkavailablility == "true")
                            {
                                Entity entity = GetEntities(_entityCode);
                                //var portcode = _PortCode;
                                CompanyVO nextStepCompany = GetUserDetails(_UserId);
                                var workFlowTaskCode = "UPDT";
                                //var vesselcall1 = _unitOfWork.Repository<VesselCall>().Find(vesselcalldata.VesselCallID);
                                var vesselcallId = data.VesselCallID;
                                var vesselcallmovementId = data.VesselCallMovementID;
                                var ETB = DateTime.Parse(data.ETB, CultureInfo.InvariantCulture);
                                var ETUB = DateTime.Parse(data.ETUB, CultureInfo.InvariantCulture);
                                var PortCode = data.PortCode;
                                var QuayCode = data.QuayCode;
                                var BerthCode = data.SheduledBerth;
                                var FromBollardCode = data.FromBollardCode;
                                //var ToBollardCode = vesselcalldata.ToBollardCode;
                                var RecordStatus = "A";
                                var ModifiedBy = _UserId;
                                var ModifiedDate = DateTime.Now;
                                var MovementStatus = "SCH";

                                //_unitOfWork.ExecuteSqlCommand("Update VesselCall set  FromPositionPortCode = @p0, FromPositionQuayCode = @p1, FromPositionBerthCode = @p2, FromPositionBollardCode = @p3, ToPositionPortCode = @p0 ,ToPositionQuayCode = @p1, ToPositionBerthCode = @p2, ToPositionBollardCode = @p4, RecordStatus = @p5, ModifiedBy = @p6, ModifiedDate =  @p7 where VesselCallID = @p8", PortCode, QuayCode, BerthCode, FromBollardCode, ToBollardCode, RecordStatus, ModifiedBy, ModifiedDate, vesselcallId);
                                //_unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1, FromPositionPortCode = @p2, FromPositionQuayCode = @p3, FromPositionBerthCode = @p4, FromPositionBollardCode = @p5, ToPositionPortCode = @p2 ,ToPositionQuayCode = @p3, ToPositionBerthCode = @p4, ToPositionBollardCode = @p6, RecordStatus = @p7, ModifiedBy = @p8, ModifiedDate =  @p9, MovementStatus = @p10 where VesselCallMovementID = @p11", ETB, ETUB, PortCode, QuayCode, BerthCode, FromBollardCode, ToBollardCode, RecordStatus, ModifiedBy, ModifiedDate, MovementStatus, vesselcallmovementId);
                                //_unitOfWork.ExecuteSqlCommand("Update VesselCall set  FromPositionPortCode = @p0, FromPositionQuayCode = @p1, FromPositionBerthCode = @p2, FromPositionBollardCode = @p3, ToPositionPortCode = @p4 ,ToPositionQuayCode = @p5, ToPositionBerthCode = @p6, ToPositionBollardCode = @p7, RecordStatus = @p8, ModifiedBy = @p9, ModifiedDate =  @p10 where VesselCallID = @p11", PortCode, QuayCode, BerthCode, FromBollardCode, toBollard.PortCode, toBollard.QuayCode, toBollard.BerthCode, toBollard.BollardCode, RecordStatus, ModifiedBy, ModifiedDate, vesselcallId);// Commented by sandeep on 21-10-2015
                                _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1, FromPositionPortCode = @p2, FromPositionQuayCode = @p3, FromPositionBerthCode = @p4, FromPositionBollardCode = @p5, ToPositionPortCode = @p6 ,ToPositionQuayCode = @p7, ToPositionBerthCode = @p8, ToPositionBollardCode = @p9, RecordStatus = @p10, ModifiedBy = @p11, ModifiedDate =  @p12, MovementStatus = @p13 where VesselCallMovementID = @p14", ETB, ETUB, PortCode, QuayCode, BerthCode, FromBollardCode, toBollard.PortCode, toBollard.QuayCode, toBollard.BerthCode, toBollard.BollardCode, RecordStatus, ModifiedBy, ModifiedDate, MovementStatus, vesselcallmovementId);

                                var vesselcallmovement = _unitOfWork.Repository<VesselCallMovement>().Find(data.VesselCallMovementID);
                                _notificationpublisher.Publish(entity.EntityID, vesselcallmovement.VesselCallMovementID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, workFlowTaskCode);
                            }
                        }
                    }
                    else
                    {
                        checkavailablility = "Berth is not suitable.";
                    }
                }

                if (data.IsScheduleStatus == false)
                {
                    checkavailablility = "true";
                    Entity entity = GetEntities(_entityCode);
                    //var portcode = _PortCode;
                    CompanyVO nextStepCompany = GetUserDetails(_UserId);
                    var workFlowTaskCode = "UPDT";
                    var vesselcallmovement = _unitOfWork.Repository<VesselCallMovement>().Find(data.VesselCallMovementID);
                    var vesselcallmovementId = data.VesselCallMovementID;
                    var MovementStatus = "CONF";
                    var SlotStatus = "PLND";
                    var ETB = DateTime.Parse(data.ETB, CultureInfo.InvariantCulture);
                    var ETUB = DateTime.Parse(data.ETUB, CultureInfo.InvariantCulture);

                    //var vesselcall1 = _unitOfWork.Repository<VesselCall>().Find(vesselcalldata.VesselCallID);
                    var vesselcallId = data.VesselCallID;
                    var MovementDateTime = DateTime.Parse(data.ETB, CultureInfo.InvariantCulture);

                    var RecordStatus = "A";
                    var ModifiedBy = _UserId;
                    var ModifiedDate = DateTime.Now;

                    //_unitOfWork.ExecuteSqlCommand("Update VesselCall set   RecordStatus = @p0, ModifiedBy = @p1, ModifiedDate =  @p2 where VesselCallID = @p3", RecordStatus, ModifiedBy, ModifiedDate, vesselcallId);// Commented by sandeep on 21-10-2015

                    //-- Modified by sandeep on 24-02-2015
                  
                    if (vesselcallmovement.MovementType == MovementTypes.SHIFTING)
                    {
                        var AutoConfiguredSlots = _servicerequestrepository.GetAutoConfiguredSlots(Convert.ToDateTime(vesselcallmovement.ETB), _PortCode);

                        SlotStatus = null;

                        //DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                        //        objVesselCallMovement.Slot = GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);
                        //        objVesselCallMovement.SlotDate = MovementStarttime;
                        Nullable<DateTime> MovementStarttime = Convert.ToDateTime(vesselcallmovement.ETB).AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                        var Slot = GetSlotPeriodBySlotdate(MovementStarttime ?? DateTime.MinValue, vesselcallmovement.FromPositionPortCode);
                        var SlotDate = DateTime.Parse(data.ETB, CultureInfo.InvariantCulture);
                        _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1, MovementStatus = @p2, SlotStatus = @p3, Slot = @p4, MovementDateTime = @p5, SlotDate = @p6, RecordStatus = @p7, ModifiedBy = @p8, ModifiedDate =  @p9  where VesselCallMovementID = @p10", ETB, ETUB, MovementStatus, SlotStatus, Slot, MovementDateTime, SlotDate, RecordStatus, ModifiedBy, ModifiedDate, vesselcallmovementId);

                        _berthPlanningRepository.ShiftingServiceRequest(data.VCN);
                    }
                    else
                    {
                        _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1, MovementStatus = @p2, SlotStatus = @p3, MovementDateTime = @p4, RecordStatus = @p5, ModifiedBy = @p6, ModifiedDate =  @p7  where VesselCallMovementID = @p8", ETB, ETUB, MovementStatus, SlotStatus, MovementDateTime, RecordStatus, ModifiedBy, ModifiedDate, vesselcallmovementId);
                    }

                    //-- end

                    _notificationpublisher.Publish(entity.EntityID, vesselcallmovement.VesselCallMovementID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, workFlowTaskCode);
                }
                return checkavailablility;
            });
        }

        private string GetSlotPeriodBySlotdate(DateTime slotdatetime, string port)
        {
            int hours = slotdatetime.Hour;
            string slotPeriod = string.Empty;
           // double totalminutes = hours * 60;
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

        //private string CheckBerthAvailability(string portCode, string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime)
        //{
        //    CultureInfo Culture = new CultureInfo("en-US");
        //    Decimal FromMeter = Convert.ToDecimal((fromBollardMeter.Trim()).Replace(',', '.'), Culture);
        //    Decimal ToMeter = Convert.ToDecimal((toBollardMeter.Trim()).Replace(',', '.'), Culture);

        //    var berthconfiguration = _berthPlanningRepository.GetBerthPlanningConfigurations(_PortCode);

        //    var safedistance = berthconfiguration.Find(s => s.ConfigName == "SAFEDISTANCE");

        //    List<AvailabilityVO> VesselCallMovements = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
        //                                                where vcm.FromPositionPortCode == portCode && vcm.FromPositionQuayCode == quayCode && vcm.MovementStatus != "MPEN" && vcm.VesselCallMovementID != Convert.ToInt32(vcmId, CultureInfo.InvariantCulture)
        //                                                select new AvailabilityVO
        //                                                {
        //                                                    VCN = vcm.VCN,
        //                                                    FromPortCode = vcm.FromPositionPortCode,
        //                                                    ToPortCode = vcm.ToPositionPortCode,
        //                                                    FromQuayCode = vcm.FromPositionQuayCode,
        //                                                    ToQuayCode = vcm.ToPositionQuayCode,
        //                                                    FromBerthCode = vcm.FromPositionBerthCode,
        //                                                    ToBerthCode = vcm.ToPositionBerthCode,
        //                                                    FromBollardMeter = vcm.FromPositionBollardCode != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.BollardCode == vcm.FromPositionBollardCode && bo.BerthCode == vcm.FromPositionBerthCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
        //                                                    ToBollardMeter = vcm.ToPositionBollardCode != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.BollardCode == vcm.ToPositionBollardCode && bo.BerthCode == vcm.ToPositionBerthCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
        //                                                    FromBollardCode = vcm.FromPositionBollardCode,
        //                                                    ToBollardCode = vcm.ToPositionBollardCode,
        //                                                    BerthTime = (vcm.ATB == null ? vcm.ETB : vcm.ATB) ?? DateTime.MinValue,
        //                                                    UnBerthTime = (vcm.ATUB == null ? vcm.ETUB : vcm.ATUB) ?? DateTime.MinValue,
        //                                                }).ToList();

        //    VesselCallMovements = VesselCallMovements.Where(vcm => (DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();
        //    VesselCallMovements = VesselCallMovements.Where(vcm => (vcm.FromBollardMeter >= FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) && vcm.FromBollardMeter <= ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture)) || ((FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) >= vcm.FromBollardMeter && (FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) <= vcm.ToBollardMeter)) || (ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) >= vcm.FromBollardMeter) && (ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) <= vcm.ToBollardMeter))).ToList();

        //    int VesselNotSafe = 0;

        //    foreach (AvailabilityVO vcm in VesselCallMovements)
        //    {
        //        decimal SafeFromPoint;
        //        decimal SafeToPoint;

        //        if (FromMeter > Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture))
        //        {
        //            SafeFromPoint = Convert.ToDecimal(FromMeter, CultureInfo.InvariantCulture) - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);
        //            SafeToPoint = Convert.ToDecimal(ToMeter, CultureInfo.InvariantCulture) + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);
        //        }
        //        else
        //        {
        //            SafeFromPoint = Convert.ToDecimal(FromMeter);
        //            SafeToPoint = Convert.ToDecimal(ToMeter, CultureInfo.InvariantCulture) + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);
        //        }

        //        var VesselSafe1 = Convert.ToDecimal(vcm.FromBollardMeter) >= Convert.ToDecimal(SafeFromPoint) && Convert.ToDecimal(vcm.FromBollardMeter) <= Convert.ToDecimal(SafeToPoint);
        //        var VesselSafe2 = Convert.ToDecimal(SafeFromPoint) >= Convert.ToDecimal(vcm.FromBollardMeter) && (Convert.ToDecimal(SafeFromPoint) <= vcm.ToBollardMeter);
        //        var VesselSafe3 = Convert.ToDecimal(SafeToPoint) >= Convert.ToDecimal(vcm.FromBollardMeter) && Convert.ToDecimal(SafeToPoint) <= Convert.ToDecimal(vcm.ToBollardMeter);

        //        if (VesselSafe1 == true || VesselSafe2 == true || VesselSafe3 == true)
        //        {
        //            VesselNotSafe += 1;
        //        }
        //    }

        //    List<BerthMaintenanceData> BerthMainList = (from b in _unitOfWork.Repository<BerthMaintenance>().Query().Select()
        //                                                where b.MaintPortCode == portCode && b.MaintQuayCode == quayCode
        //                                                select new BerthMaintenanceData
        //                                                {
        //                                                    BerthMaintenanceID = b.BerthMaintenanceID,
        //                                                    MaintPortCode = b.MaintPortCode,
        //                                                    MaintQuayCode = b.MaintQuayCode,
        //                                                    MaintBerthCode = b.MaintBerthCode,
        //                                                    FromBerthCode = b.FromBerthCode,
        //                                                    FromBollard = b.FromBollard,
        //                                                    ToBerthCode = b.ToBerthCode,
        //                                                    ToBollard = b.ToBollard,
        //                                                    PeriodFrom = b.PeriodFrom,
        //                                                    PeriodTo = b.PeriodTo,
        //                                                    FromBollardMeter = b.FromBollard != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.BollardCode == b.FromBollard && bo.BerthCode == b.FromBerthCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
        //                                                    ToBollardMeter = b.ToBollard != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.BollardCode == b.ToBollard && bo.BerthCode == b.ToBerthCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
        //                                                    WorkflowInstanceId = b.WorkflowInstanceId
        //                                                }).ToList();

        //    BerthMainList = BerthMainList.Where(bm => (DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();
        //    BerthMainList = BerthMainList.Where(bm => (bm.FromBollardMeter >= FromMeter && bm.FromBollardMeter <= ToMeter) || ((FromMeter >= bm.FromBollardMeter) && (FromMeter <= bm.ToBollardMeter)) || ((ToMeter >= bm.FromBollardMeter) && (ToMeter <= bm.ToBollardMeter))).ToList();

        //    List<BerthMaintenanceData> BerthMain = new List<BerthMaintenanceData>();

        //    List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Query().Select().Where(s => s.RecordStatus == "A" && s.VCN == vcn).Select(s => s.Reason).ToList<String>();
        //    Boolean IsBerthMaintainence = false;
        //    if (BerthMainList.Count > 0)
        //    {
        //        foreach (BerthMaintenanceData bm in BerthMainList)
        //        {
        //            List<string> BerthReasonforVisit = _unitOfWork.Repository<BerthReasonForVisit>().Query().Select().Where(b => b.PortCode == bm.MaintPortCode && b.QuayCode == bm.MaintQuayCode && b.BerthCode == bm.MaintBerthCode && b.RecordStatus == "A").Select(b => b.ReasonForVisitCode).ToList<String>();
        //            foreach (string ArrivalReason in ArrivalReasons)
        //            {
        //                Boolean ContainsReason = BerthReasonforVisit.Contains(ArrivalReason);
        //                if (ContainsReason == true)
        //                {
        //                    IsBerthMaintainence = false;
        //                    BerthMain.Add(bm);

        //                }
        //                else
        //                {
        //                    IsBerthMaintainence = true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        IsBerthMaintainence = false;
        //    }

        //    ConflictingData _ConflictingData = new ConflictingData();
        //    _ConflictingData.MaintainenceData = BerthMain;
        //    _ConflictingData.VesselsColliding = VesselCallMovements;

        //    Int32 count = VesselCallMovements.Count;
        //    Int32 bmcount = BerthMainList.Count;

        //    if (VesselNotSafe > 0 || count > 0 || bmcount > 0)
        //    {
        //        return "Safe Distance is not maintained.";
        //    }
        //    else
        //    {
        //        return "true";
        //    }
        //}

        private string CheckBerthAvailability(string portCode, string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime)
        {

            CultureInfo Culture = new CultureInfo("en-US");

            Decimal FromMeter = Convert.ToDecimal((fromBollardMeter.Trim()).Replace(',', '.'), Culture);

            Decimal ToMeter = Convert.ToDecimal((toBollardMeter.Trim()).Replace(',', '.'), Culture);

            var berthconfiguration = _berthPlanningRepository.GetBerthPlanningConfigurations(_PortCode);

            var safedistance = berthconfiguration.Find(s => s.ConfigName == "SAFEDISTANCE");

            int vcmIdConverted = Convert.ToInt32(vcmId, CultureInfo.InvariantCulture);

            var bollardListAtPortAndQuay = (from bollard in _unitOfWork.Repository<Bollard>().Queryable()

                                            where bollard.PortCode == portCode && bollard.QuayCode == quayCode
                                            select bollard).ToList<Bollard>();

            List<AvailabilityVO> VesselCallMovements = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable()

                                                        where vcm.FromPositionPortCode == portCode && vcm.FromPositionQuayCode == quayCode && vcm.MovementStatus != "MPEN" && vcm.VesselCallMovementID != vcmIdConverted
                                                        select new
                                                        AvailabilityVO
                                                        {
                                                            VCN = vcm.VCN,
                                                            FromPortCode = vcm.FromPositionPortCode,
                                                            ToPortCode = vcm.ToPositionPortCode,
                                                            FromQuayCode = vcm.FromPositionQuayCode,
                                                            ToQuayCode = vcm.ToPositionQuayCode,
                                                            FromBerthCode = vcm.FromPositionBerthCode,
                                                            ToBerthCode = vcm.ToPositionBerthCode,
                                                            FromBollardMeter = vcm.FromPositionBollardCode != null ? 1 : 0,
                                                            ToBollardMeter = vcm.ToPositionBollardCode != null ? 1 : 0,
                                                            FromBollardCode = vcm.FromPositionBollardCode,
                                                            ToBollardCode = vcm.ToPositionBollardCode,
                                                            BerthTime = (vcm.ATB == null ? vcm.ETB : vcm.ATB) ?? DateTime.MinValue,
                                                            UnBerthTime = (vcm.ATUB == null ? vcm.ETUB : vcm.ATUB) ?? DateTime.MinValue,
                                                        }).ToList();


            VesselCallMovements = VesselCallMovements.Where(vcm => (DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();

            //TODO:  This needs to be enhanced....doing it as quick fix for task # 14447.  This is not efficiency

            foreach (AvailabilityVO vcm in VesselCallMovements)
            {
                if (vcm.FromBollardMeter == 1)
                {
                    vcm.FromBollardMeter = bollardListAtPortAndQuay.Where(a => a.BollardCode == vcm.FromBollardCode && a.BerthCode == vcm.FromBerthCode)
                                            .Select(a => a.FromMeter).FirstOrDefault();
                }

                if (vcm.ToBollardMeter == 1)
                {
                    vcm.ToBollardMeter = bollardListAtPortAndQuay.Where(a => a.BollardCode == vcm.ToBollardCode && a.BerthCode == vcm.ToBerthCode)
                                        .Select(a => a.FromMeter).FirstOrDefault();
                }
            }

            VesselCallMovements = VesselCallMovements.Where(vcm => (vcm.FromBollardMeter >= FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) && vcm.FromBollardMeter <= ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture)) || ((FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) >= vcm.FromBollardMeter && (FromMeter - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) <= vcm.ToBollardMeter)) || (ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) >= vcm.FromBollardMeter) && (ToMeter + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture) <= vcm.ToBollardMeter))).ToList();

            int VesselNotSafe = 0;

            foreach (AvailabilityVO vcm in VesselCallMovements)
            {
                decimal SafeFromPoint;
                decimal SafeToPoint;

                if (FromMeter > Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture))
                {
                    SafeFromPoint = Convert.ToDecimal(FromMeter, CultureInfo.InvariantCulture) - Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);

                    SafeToPoint = Convert.ToDecimal(ToMeter, CultureInfo.InvariantCulture) + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);
                }
                else
                {
                    SafeFromPoint = Convert.ToDecimal(FromMeter);

                    SafeToPoint = Convert.ToDecimal(ToMeter, CultureInfo.InvariantCulture) + Convert.ToDecimal(safedistance.ConfigValue, CultureInfo.InvariantCulture);
                }

                var VesselSafe1 = Convert.ToDecimal(vcm.FromBollardMeter) >= Convert.ToDecimal(SafeFromPoint) && Convert.ToDecimal(vcm.FromBollardMeter) <= Convert.ToDecimal(SafeToPoint);
                var VesselSafe2 = Convert.ToDecimal(SafeFromPoint) >= Convert.ToDecimal(vcm.FromBollardMeter) && (Convert.ToDecimal(SafeFromPoint) <= vcm.ToBollardMeter);
                var VesselSafe3 = Convert.ToDecimal(SafeToPoint) >= Convert.ToDecimal(vcm.FromBollardMeter) && Convert.ToDecimal(SafeToPoint) <= Convert.ToDecimal(vcm.ToBollardMeter);

                if (VesselSafe1 == true || VesselSafe2 == true || VesselSafe3 == true)
                {
                    VesselNotSafe += 1;
                }
            }

            List<BerthMaintenanceData> BerthMainList = (from b in _unitOfWork.Repository<BerthMaintenance>().Query(b => b.MaintPortCode == portCode && b.MaintQuayCode == quayCode && b.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved && b.RecordStatus == RecordStatus.Active)
                                                        .Include(b => b.WorkflowInstance)
                                                            .Select()
                                                        select new
                                                        BerthMaintenanceData
                                                        {
                                                            BerthMaintenanceID = b.BerthMaintenanceID,
                                                            MaintPortCode = b.MaintPortCode,
                                                            MaintQuayCode = b.MaintQuayCode,
                                                            MaintBerthCode = b.MaintBerthCode,
                                                            FromBerthCode = b.FromBerthCode,
                                                            FromBollard = b.FromBollard,
                                                            ToBerthCode = b.ToBerthCode,
                                                            ToBollard = b.ToBollard,
                                                            PeriodFrom = b.PeriodFrom,
                                                            PeriodTo = b.PeriodTo,
                                                            FromBollardMeter = b.FromBollard != null ? 1 : 0,
                                                            ToBollardMeter = b.ToBollard != null ? 1 : 0,
                                                            WorkflowInstanceId = b.WorkflowInstanceId
                                                        }).ToList();

            BerthMainList = BerthMainList.Where(bm => (DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();

            //TODO:  This needs to be enhanced....doing it as quick fix for task # 14447.  This is not efficiency

            foreach (BerthMaintenanceData bm in BerthMainList)
            {
                if (bm.FromBollardMeter == 1)
                {
                    bm.FromBollardMeter = bollardListAtPortAndQuay.Where(a => a.BollardCode == bm.FromBollard && a.BerthCode == bm.FromBerthCode)
                        .Select(a => a.FromMeter).FirstOrDefault();
                }

                if (bm.ToBollardMeter == 1)
                {
                    bm.ToBollardMeter = bollardListAtPortAndQuay.Where(a => a.BollardCode == bm.ToBollard && a.BerthCode == bm.ToBerthCode)
                        .Select(a => a.FromMeter).FirstOrDefault();
                }
            }

            BerthMainList = BerthMainList.Where(bm => (bm.FromBollardMeter >= FromMeter && bm.FromBollardMeter <= ToMeter) || ((FromMeter >= bm.FromBollardMeter) && (FromMeter <= bm.ToBollardMeter)) || ((ToMeter >= bm.FromBollardMeter) && (ToMeter <= bm.ToBollardMeter))).ToList();

            List<BerthMaintenanceData> BerthMain = new List<BerthMaintenanceData>();

            List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Query().Select().Where(s => s.RecordStatus == "A" && s.VCN == vcn).Select(s => s.Reason).ToList<String>();

            Boolean IsBerthMaintainence = false;

            if (BerthMainList.Count > 0)
            {
                foreach (BerthMaintenanceData bm in BerthMainList)
                {
                    List<string> BerthReasonforVisit = _unitOfWork.Repository<BerthReasonForVisit>().Query().Select().Where(b => b.PortCode == bm.MaintPortCode && b.QuayCode == bm.MaintQuayCode && b.BerthCode == bm.MaintBerthCode && b.RecordStatus == "A").Select(b => b.ReasonForVisitCode).ToList<String>();

                    foreach (string ArrivalReason in ArrivalReasons)
                    {
                        Boolean ContainsReason = BerthReasonforVisit.Contains(ArrivalReason);

                        if (ContainsReason == true)
                        {
                            IsBerthMaintainence = false;
                            BerthMain.Add(bm);
                        }
                        else
                        {
                            IsBerthMaintainence = true;
                        }
                    }
                }
            }
            else
            {
                IsBerthMaintainence = false;
            }

            ConflictingData _ConflictingData = new ConflictingData();

            _ConflictingData.MaintainenceData = BerthMain;

            _ConflictingData.VesselsColliding = VesselCallMovements;

            Int32 count = VesselCallMovements.Count;
            Int32 bmcount = BerthMainList.Count;

            if (VesselNotSafe > 0 || count > 0 || bmcount > 0)
            {
                return "Safe Distance is not maintained.";
            }
            else
            {
                return "true";
            }
        }
    }
}

