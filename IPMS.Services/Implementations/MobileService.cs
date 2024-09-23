using System.Collections.Generic;
using System.Linq;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Repository;
using System;
using System.Data.SqlClient;
using IPMS.Domain;
using System.Data.Entity;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MobileService : ServiceBase, IMobileService
    {
        private IModuleRepository _moduleRepository;
        private ISystemNotificationRepository _systemNotificationRepository;
        private IPortRepository _portRepository;
        private IEntityRepository _entityRepository;

        public MobileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _moduleRepository = new ModuleRepository(_unitOfWork);
            _systemNotificationRepository = new SystemNotificationRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
        }

        public MobileService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _moduleRepository = new ModuleRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _systemNotificationRepository = new SystemNotificationRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
        }

        /// <summary>
        /// To get the mobile module list
        /// </summary>
        /// <returns>List of mobile modules</returns>
        public IEnumerable<MobileModuleVO> GetModulesForMobile()
        {

            return EncloseTransactionAndHandleException(() =>
            {
                var modules = _moduleRepository.GetMobileModules();

                string NotPrtCode = "";

                if (string.IsNullOrEmpty(_PortCode))
                {
                    var port = _portRepository.GetPortsByUser(_UserId);
                    NotPrtCode = port[0].PortCode;
                }
                else
                {
                    NotPrtCode = _PortCode;
                }

                foreach (var module in modules)
                {

                    if (module.OrderNo == 1)
                    {
                        var result = _systemNotificationRepository.GetNotifications(NotPrtCode, _UserId);

                        module.Count = result.Count;
                    }

                }

                return modules.MapToListDTOMobile();
            });
        }

        /// <summary>
        /// This function will return the list of new and old notifications
        /// </summary>
        /// <returns>Returns the list of notifications</returns>
        public IEnumerable<SystemNotificationVO> GetNotifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var notifications = _systemNotificationRepository.GetNotifications(_PortCode, _UserId);

                return notifications.MapToDTO();
            });
        }


        /// <summary>
        /// This function will return the list of new notifications
        /// </summary>
        /// <returns>Returns the list of new notifications</returns>
        public IEnumerable<SystemNotificationVO> GetNewNotifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var newNotifications = _systemNotificationRepository.GetNotifications(_PortCode, _UserId);

                return newNotifications.MapToDTO();
            });
        }

        /// <summary>
        /// This function is used to return a features entity
        /// </summary>
        /// <returns>Features entity will return</returns>
        public IEnumerable<EntityVO> GetFeatures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var features = _entityRepository.GetFeaturesEntity();

                return features;
            });
        }

        /// <summary>
        /// This function is used to change the notification status
        /// </summary>
        /// <param name="notificationData">SystemNotification object</param>
        /// <returns>returns NotificationId</returns>
        public int ModifyNotifications(SystemNotification notificationData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                notificationData.IsRead = "Y";
                notificationData.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SystemNotification>().Update(notificationData);
                _unitOfWork.SaveChanges();

                return notificationData.NotificationId;
            });
        }

        /// <summary>
        /// This function is used to change the notification status
        /// </summary>
        /// <param name="notificationData">SystemNotification object</param>
        /// <returns>returns NotificationId</returns>
        public int ModifyNotificationsByID(string notificationID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int valOfnotificationID = int.Parse(notificationID, System.Globalization.CultureInfo.InvariantCulture);
                var objSystemNotification = (from s in _unitOfWork.Repository<SystemNotification>().Query().Select()
                                             where s.NotificationId == valOfnotificationID && s.UserID == _UserId
                                             select s).FirstOrDefault();

                if (objSystemNotification != null)
                {
                    objSystemNotification.IsRead = "Y";
                    objSystemNotification.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<SystemNotification>().Update(objSystemNotification);
                    _unitOfWork.SaveChanges();
                }
                return objSystemNotification.NotificationId;
            });
        }


        public IEnumerable<PlannedMovementsVO> GetPlannedMovements()
        {
            return ExecuteFaultHandledOperation(() =>
          {

              //var plannedMovements = (from v in _unitOfWork.Repository<VesselCallMovement>().Query()
              //                        .Include(v => v.ArrivalNotification)
              //                        .Include(v => v.ArrivalNotification.Vessel)
              //                        .Select()
              //                        join sc in _unitOfWork.Repository<SubCategory>().Query().Select()
              //                        on v.MovementType equals sc.SubCatCode
              //                        where (v.MovementStatus == "CONF" || v.MovementStatus == "BRTH") && Convert.ToDateTime(v.MovementDateTime).Date == DateTime.Now.Date
              //                        select new VesselCallMovementVO
              //                        {
              //                            MovementTime = v.MovementDateTime.ToString(),
              //                            MovementType = sc.SubCatName,
              //                            VesselName = v.ArrivalNotification != null ? (v.ArrivalNotification.Vessel != null ? v.ArrivalNotification.Vessel.VesselName : null) : null

              //                        }).ToList();

              var portCode = new SqlParameter("@portcode ", _PortCode);

              var plannedMovements = _unitOfWork.SqlQuery<PlannedMovementsVO>("dbo.usp_MobilePlannedMovements @portcode", portCode).ToList();


              return plannedMovements;
          });
        }

        public IEnumerable<PlannedMovementsVO> GetPlannedMovementsForDesktop(string PortCode)
        {

            return ExecuteFaultHandledOperation(() =>
            {

                var portCode = new SqlParameter("@portcode ", PortCode);
                var LoginId = new SqlParameter("@UserId", _UserId);
                var plannedmvnts = _unitOfWork.SqlQuery<PlannedMovementsVO>("dbo.usp_PlannedMovements @portcode ,@UserId", portCode, LoginId).ToList();

                return plannedmvnts;
            });

            //DateTime date = DateTime.Today;
            //var plannedmvnts = (from v in _unitOfWork.Repository<VesselCallMovement>().Queryable()
            //                    join a in _unitOfWork.Repository<ArrivalNotification>().Queryable()
            //                    on v.VCN equals a.VCN

            //                    join ve in _unitOfWork.Repository<Vessel>().Queryable()
            //                    on a.VesselID equals ve.VesselID
            //                    join ar in _unitOfWork.Repository<ArrivalReason>().Queryable() on a.VCN equals ar.VCN
            //                    join sc in _unitOfWork.Repository<SubCategory>().Queryable() on ar.Reason equals sc.SubCatCode
            //                    join su in _unitOfWork.Repository<SubCategory>().Queryable() on ve.VesselType equals su.SubCatCode
            //                                               into temp
            //                    from t in temp.DefaultIfEmpty()


            //                    join ag in _unitOfWork.Repository<Agent>().Queryable() on a.AgentID equals ag.AgentID
            //                    into temp1
            //                    from t1 in temp1.DefaultIfEmpty()

            //                    join s in _unitOfWork.Repository<ServiceRequest>().Queryable() on v.ServiceRequestID equals s.ServiceRequestID

            //                    join sr in _unitOfWork.Repository<SubCategory>().Queryable() on s.MovementType equals sr.SubCatCode


            //                    join slr in _unitOfWork.Repository<SubCategory>().Queryable() on v.SlotStatus equals slr.SubCatCode
            //                    into temp2
            //                    from t2 in temp2.DefaultIfEmpty()
            //                    //join ra in _unitOfWork.Repository<ResourceAllocation>().Queryable() on s.ServiceRequestID equals ra.ServiceReferenceID
            //                    //into temp3
            //                    //from t3 in temp3.ToList().Distinct()
            //                    where (v.MovementType == MovementTypes.ARRIVAL || v.MovementType == MovementTypes.SAILING || v.MovementType == MovementTypes.SHIFTING || v.MovementType == MovementTypes.WARPING) && v.MovementDateTime.Value >= date && v.RecordStatus == RecordStatus.Active && a.PortCode == PortCode
            //                    select new PlannedMovementsVO
            //                    {
            //                        MovementType = s.SubCategory.SubCatName,
            //                        VesselName = ve.VesselName,
            //                        LOA = ve.LengthOverallInM,
            //                        GRT = ve.GrossRegisteredTonnageInMT,
            //                        RegisteredName = t1.RegisteredName,
            //                        ArrDraft = a.ArrDraft,
            //                        DepDraft = a.DepDraft,
            //                        Draft = a.ArrDraft + " / " + a.DepDraft,
            //                        BerthName = v.FromPositionBerthCode ?? " ",
            //                        MovementDateTime = v.MovementDateTime,
            //                        ScheduledTime = v.ModifiedDate,
            //                        Status=v.SubCategory1.SubCatName,
            //                        //Status=(ra.TaskStatus=="COMP")?"Completed":v.SubCategory1.SubCatName,
            //                        //Status = (v.SlotStatus == "SCHD" || v.MovementStatus == "CONF") || (v.MovementStatus == "BERT" || v.MovementStatus == "SALD") ? "Completed" : v.SubCategory1.SubCatName,
            //                        ReasonforvisitName = a.ReasonForVisit,
            //                        VeselType = ve.VesselType

            //                    }
            //               ).ToList();

          
            //List<PlannedMovementsVO> plannedMovementsVOs = new List<PlannedMovementsVO>();
            //if (plannedmvnts != null)
            //{
            //    plannedmvnts.ToList().ForEach(val =>
            //    {
            //        //if (val != null)
            //        //{
            //        //val.Draft =val.ArrDraft >= val.DepDraft ? val.ArrDraft : val.DepDraft;
            //        if (val.ArrDraft == string.Empty || val.ArrDraft == null)
            //        {
            //            val.ArrDraft = "0";
            //        }
            //        if (val.DepDraft == string.Empty || val.DepDraft == null)
            //        {
            //            val.DepDraft = "0";
            //        }
            //        val.Draft = Convert.ToDecimal(val.ArrDraft) >= Convert.ToDecimal(val.DepDraft) ? val.ArrDraft : val.DepDraft;
            //        //plannedMovementsVOs.Add(val);
            //        //}
            //    });
            //}
           //return plannedmvnts;
            //return plannedmvnts;
        }

        public IEnumerable<PlannedMovementsVO> GetPlannedMovementsForAnonymous(string portCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var PortCode = new SqlParameter("@portcode ", portCode);

                var plannedmvnts = _unitOfWork.SqlQuery<PlannedMovementsVO>("dbo.usp_PlannedMovementsForAnonymous @portcode", PortCode).ToList();

                return plannedmvnts;
            });
        }

        //By Mahesh : To show VCN status For Mobile App.
        public List<ArrvNotfMobileAppVo> GetVCNStatusForMobApp(string VCN)
        {

            var vcn = new SqlParameter("@p_VCN", VCN);

            List<ArrvNotfMobileAppVo> Result = _unitOfWork.SqlQuery<ArrvNotfMobileAppVo>("dbo.usp_ArrivalNotificationStatusForMobile @p_VCN", vcn).ToList();

            return Result;

        }
    }
}
