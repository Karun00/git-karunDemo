using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Data.SqlClient;
using System;
using Core.Repository.Providers.EntityFramework;
using IPMS.Core.Repository.Exceptions;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VesselETAChangeService : ServiceBase, IVesselETAChangeService
    {
        private INotificationPublisher notificationpublisher;
        private IPortConfigurationRepository portConfigurationRepository;
        private IVesselETAChangeRepository vesseletachangeRepository;
        private IEntityRepository entityRepository;
        private IUserRepository userRepository;
        private ISAPPostingRepository sapPostingRepository;

        private const string _entityCode = EntityCodes.Vessel_ETAChange;

        public VesselETAChangeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            vesseletachangeRepository = new VesselETAChangeRepository(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            sapPostingRepository = new SAPPostingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }

        public VesselETAChangeService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vesseletachangeRepository = new VesselETAChangeRepository(_unitOfWork);
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            sapPostingRepository = new SAPPostingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }

        #region GetArrivalVCNS
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        public List<VesselETAChangeVO> GetArrivalVcns()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int agentid = 0;
                if (_UserType == "AGNT")
                {
                    agentid = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId).AgentID;
                }
                if (agentid > 0)
                {
                    return vesseletachangeRepository.GetArrivalVcnsOnAgentBased(_PortCode, agentid);
                }
                else
                {
                    return vesseletachangeRepository.GetArrivalVcns(_PortCode);
                }
            });
        }
        #endregion

        #region GetVesselInfoByVCN
        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        public VesselETAChangeVO GetVesselInfoByVcns(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return vesseletachangeRepository.GetVesselInfoByVcn(vcn);
            });
        }
        #endregion

        #region PostVesselETAChange
        /// <summary>
        /// To Add Change ETA Data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public VesselETAChangeVO PostVesselEtaChange(VesselETAChangeVO obj)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                VesselETAChange eta = new VesselETAChange();
                var entityid = entityRepository.GetEntitiesNotification(EntityCodes.Vessel_ETAChange).EntityID;
                var portcode = _PortCode;
                var nextStepCompany = userRepository.GetUserDetails(_UserId);

                obj.CreatedBy = _UserId;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedBy = _UserId;
                obj.ModifiedDate = DateTime.Now;

                VesselCall objVesselcall = _unitOfWork.Repository<VesselCall>().Queryable().Where(t => t.VCN == obj.VCN).FirstOrDefault();


                obj.OldETA = Convert.ToString(objVesselcall.ETA, CultureInfo.InvariantCulture);
                obj.OldETD = Convert.ToString(objVesselcall.ETD, CultureInfo.InvariantCulture);

                if (obj.PlanDateTimeOfBerth != null)
                    obj.OldPlanDateTimeOfBerth = Convert.ToDateTime(obj.PlanDateTimeOfBerth, CultureInfo.InvariantCulture);
                if (obj.PlanDateTimeToStartCargo != null)
                    obj.OldPlanDateTimeToStartCargo = Convert.ToDateTime(obj.PlanDateTimeToStartCargo, CultureInfo.InvariantCulture);
                if (obj.PlanDateTimeToCompleteCargo != null)
                    obj.OldPlanDateTimeToCompleteCargo = Convert.ToDateTime(obj.PlanDateTimeToCompleteCargo, CultureInfo.InvariantCulture);
                if (obj.PlanDateTimeToVacateBerth != null)
                    obj.OldPlanDateTimeToVacateBerth = Convert.ToDateTime(obj.PlanDateTimeToVacateBerth, CultureInfo.InvariantCulture);


                eta = VesselETAChangeMapExtension.MapToEntity(obj);
                eta.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<VesselETAChange>().Insert(eta);

                var VCN = new SqlParameter("@VCN", obj.VCN);
                var NewETA = Convert.ToDateTime(obj.NewETA, CultureInfo.InvariantCulture);
                var NewETD = Convert.ToDateTime(obj.NewETD, CultureInfo.InvariantCulture);


                var PlanBerth = eta.PlanDateTimeOfBerth;
                var PlanStartCargo = eta.PlanDateTimeToStartCargo;
                var PlanCompCargo = eta.PlanDateTimeToCompleteCargo;
                var PlanVacBerth = eta.PlanDateTimeToVacateBerth;

                var etachanges = obj.NoofTimesETAChanged + 1;

                if ((!string.IsNullOrEmpty(obj.ATA) || !string.IsNullOrEmpty(obj.ATB)) &&
                    (!string.IsNullOrEmpty(obj.ATD) || !string.IsNullOrEmpty(obj.ATUB)))
                {
                    throw new BusinessExceptions("VCN already closed, nothing to update.");
                }

                if ((!string.IsNullOrEmpty(obj.ATA) || !string.IsNullOrEmpty(obj.ATB)))
                {
                    #region Issue Fix : When ETD is updated Berth Planning Not updating for Shifting Request

                    string message = CheckAnyPendingServiceRequest(obj.VCN, NewETA, NewETD,
                        Convert.ToDateTime(obj.OldETA, CultureInfo.InvariantCulture),
                        Convert.ToDateTime(obj.OldETD, CultureInfo.InvariantCulture), etachanges, PlanBerth, PlanStartCargo, PlanCompCargo, PlanVacBerth, obj.VoyageIn, obj.VoyageOut);

                    if (!string.IsNullOrEmpty(message))
                    {
                        throw new BusinessExceptions(message);
                    }

                    #endregion

                    #region Old Code - Commented
                    _unitOfWork.ExecuteSqlCommand("Update VesselCall set  ETD = @p0, ETUB = @p0, NoofTimesETAChanged = @p1, ModifiedBy=@p2, ModifiedDate=getdate() where VCN = @p3", NewETD, etachanges, _UserId, obj.VCN);
                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set  ETUB = @p0, ModifiedBy=@p1, ModifiedDate=getdate() where VCN = @p2 and MovementType != 'SHMV'", NewETD, _UserId, obj.VCN);
                    _unitOfWork.ExecuteSqlCommand("Update ArrivalNotification set  ETD = @p0, PlanDateTimeOfBerth = @p1, PlanDateTimeToStartCargo = @p2, PlanDateTimeToCompleteCargo = @p3, PlanDateTimeToVacateBerth = @p4, ModifiedBy=@p5, ModifiedDate=getdate(), VoyageIn =@p7, VoyageOut=@p8 where VCN = @p6 ", NewETD, PlanBerth, PlanStartCargo, PlanCompCargo, PlanVacBerth, _UserId, obj.VCN, obj.VoyageIn, obj.VoyageOut);
                    List<ServiceRequestChangeETAVO> service = _unitOfWork.SqlQuery<ServiceRequestChangeETAVO>("Select * from  ServiceRequest where VCN = @p0 and RecordStatus='A' and IsRecordingCompleted='N'", obj.VCN).ToList();

                    if (service.Count > 0)
                    {
                        var vesselCallMovements = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(vc=>vc.VCN == obj.VCN && vc.MovementType ==MovementTypes.SHIFTING && vc.RecordStatus == RecordStatus.Active )
                                                   select vcm).ToList();

                        var shiftingNotifications = vesselCallMovements.FindAll(vcm => vcm.ServiceRequest.WorkflowInstanceId == null).ToList();
                        if (shiftingNotifications.Count > 0)
                        {
                            var shiftingNotificationsetaMinutes = (Convert.ToDateTime(obj.NewETA) - Convert.ToDateTime(obj.ETA)).TotalMinutes;

                            foreach (var vcm in shiftingNotifications)
                            {
                                var etub = Convert.ToDateTime(vcm.ETUB).AddMinutes(shiftingNotificationsetaMinutes);

                                if (vcm.ETUB <= NewETD)
                                {
                                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set MovementStatus = 'MPEN', ETUB = @p0, FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, ModifiedBy=@p1, ModifiedDate=getdate() where ServiceRequestID = @p2", NewETD, _UserId, vcm.ServiceRequestID);
                                }
                                else
                                {
                                    _unitOfWork.ExecuteSqlCommand("Update ServiceRequest set RecordStatus = 'I', ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1", _UserId, vcm.ServiceRequestID);
                                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set MovementStatus = 'MPEN', FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, RecordStatus = 'I', ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1", _UserId, vcm.ServiceRequestID);
                                }
                            }
                        }

                        var shiftingMovement = vesselCallMovements.FindAll(vcm => vcm.ServiceRequest.WorkflowInstanceId > 0).ToList();

                        if (shiftingMovement.Count > 0)
                        {
                            var shiftingEtaMinutes = (Convert.ToDateTime(obj.NewETA) - Convert.ToDateTime(obj.ETA))
                                .TotalMinutes;

                            foreach (var vcm in shiftingMovement)
                            {
                                var etub = Convert.ToDateTime(vcm.ETUB).AddMinutes(shiftingEtaMinutes);

                                if (vcm.ETUB <= NewETD)
                                {
                                    _unitOfWork.ExecuteSqlCommand(
                                        "Update VesselCallMovement set MovementStatus = 'MPEN', ETUB = @p0, FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, ModifiedBy=@p1, ModifiedDate=getdate() where ServiceRequestID = @p2",
                                        NewETD, _UserId, vcm.ServiceRequestID);
                                }
                                else
                                {
                                    _unitOfWork.ExecuteSqlCommand(
                                        "Update ServiceRequest set RecordStatus = 'I', ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1",
                                        _UserId, vcm.ServiceRequestID);
                                    _unitOfWork.ExecuteSqlCommand(
                                        "Update VesselCallMovement set MovementStatus = 'MPEN', FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, RecordStatus = 'I', ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1",
                                        _UserId, vcm.ServiceRequestID);
                                }
                            }
                        }
                    }
                    else //Updating ETUB with updated ETD for already completed Shifting Movements
                    {
                        _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set  ETUB = @p0, ModifiedBy=@p1, ModifiedDate=getdate() where VCN = @p2 and MovementType = 'SHMV'", NewETD, _UserId, obj.VCN);
                    }

                    #endregion
                }
                else
                {
                    #region Issue Fix : When ETD is updated Berth Planning Not updating

                    string message = CheckAnyPendingServiceRequest(obj.VCN, NewETA, NewETD,
                        Convert.ToDateTime(obj.OldETA, CultureInfo.InvariantCulture),
                        Convert.ToDateTime(obj.OldETD, CultureInfo.InvariantCulture), etachanges, PlanBerth, PlanStartCargo, PlanCompCargo, PlanVacBerth, obj.VoyageIn, obj.VoyageOut);

                    if (!string.IsNullOrEmpty(message))
                    {
                        throw new BusinessExceptions(message);
                    }
                    #endregion

                    #region Old Code
                    _unitOfWork.ExecuteSqlCommand("Update VesselCall set ETA = @p0, ETD = @p1, ETB = @p0, ETUB = @p1, NoofTimesETAChanged = @p2, FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, ModifiedBy=@p3, ModifiedDate=getdate() where VCN = @p4", NewETA, NewETD, etachanges, _UserId, obj.VCN);
                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set MovementStatus = 'MPEN', ETB = @p0, ETUB = @p1, FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, ModifiedBy=@p2, ModifiedDate=getdate() where VCN = @p3 and MovementType != 'SHMV'", NewETA, NewETD, _UserId, obj.VCN);
                    _unitOfWork.ExecuteSqlCommand("Update ArrivalNotification set ETA = @p0, ETD = @p1, PlanDateTimeOfBerth = @p2, PlanDateTimeToStartCargo = @p3, PlanDateTimeToCompleteCargo = @p4, PlanDateTimeToVacateBerth = @p5, ModifiedBy=@p6, ModifiedDate=getdate(), VoyageIn =@p8, VoyageOut=@p9 where VCN = @p7 ", NewETA, NewETD, PlanBerth, PlanStartCargo, PlanCompCargo, PlanVacBerth, _UserId, obj.VCN, obj.VoyageIn, obj.VoyageOut);

                    List<ServiceRequestChangeETAVO> service = _unitOfWork.SqlQuery<ServiceRequestChangeETAVO>("Select * from  ServiceRequest where VCN = @p0 and RecordStatus='A' and IsRecordingCompleted='N'", obj.VCN).ToList();

                    if (service.Count > 0)
                    {

                        var vesselCallMovements = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(vc => vc.VCN == obj.VCN && vc.MovementType == MovementTypes.SHIFTING && vc.RecordStatus == RecordStatus.Active)
                            select vcm).ToList();

                        if (vesselCallMovements.Count > 0)
                        {
                            var etaMinutes = (Convert.ToDateTime(obj.NewETA) - Convert.ToDateTime(obj.ETA)).TotalMinutes;

                            foreach (var vcm in vesselCallMovements)
                            {
                                var etb = Convert.ToDateTime(vcm.ServiceRequest.MovementDateTime).AddMinutes(etaMinutes);

                                if (etb >= NewETA && etb <= NewETD)
                                {
                                    _unitOfWork.ExecuteSqlCommand("Update ServiceRequest set MovementDateTime = @p0, ModifiedBy=@p1, ModifiedDate=getdate() where ServiceRequestID = @p2", etb, _UserId, vcm.ServiceRequestID);
                                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set MovementStatus = 'MPEN', MovementDateTime = @p0, ETB = @p0, ETUB = @p1, FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, ModifiedBy=@p2, ModifiedDate=getdate() where ServiceRequestID = @p3", etb, NewETD, _UserId, vcm.ServiceRequestID);
                                }
                                else
                                {
                                    _unitOfWork.ExecuteSqlCommand("Update ServiceRequest set RecordStatus = 'I', ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1", _UserId, vcm.ServiceRequestID);
                                    _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set MovementStatus = 'MPEN', FromPositionQuayCode = null, FromPositionBerthCode = null, FromPositionBollardCode = null, ToPositionQuayCode = null, ToPositionBerthCode = null, ToPositionBollardCode = null, RecordStatus = 'I' , ModifiedBy=@p0, ModifiedDate=getdate() where ServiceRequestID = @p1", _UserId, vcm.ServiceRequestID);
                                }
                            }
                        }

                        var lstVesselCallMovements = _unitOfWork.SqlQuery<VesselCallMovement>("select * from VesselCallMovement where VCN = @p0", obj.VCN).ToList();

                        if (lstVesselCallMovements.Count > 0)
                        {
                            foreach (var sr in lstVesselCallMovements)
                            {
                                _unitOfWork.ExecuteSqlCommand("update ResourceAllocation set ResourceID = @p0, TaskStatus = @P1, RecordStatus = @p2, CraftID = @p0 where ServiceReferenceID = @p3 and ServiceReferenceType = @p4", null, ResourceAllcationWorkFlowStatus.Pending, RecordStatus.InActive, sr.ServiceRequestID, ServiceReferenceType.VeselTraficServices);
                            }
                        }
                    }

                    #endregion

                    VesselETAChangeVO _objvesselETAChange = new VesselETAChangeVO();
                    _objvesselETAChange.VCN = obj.VCN;
                    _objvesselETAChange.SuppServiceRequestID = Convert.ToInt32(0);
                    _objvesselETAChange.CancelRemarks = "";
                    _objvesselETAChange.CreatedBy = _UserId;

                    var suppproc = new VesselETAChangeVO.SuppServRequestCancel_proc(_objvesselETAChange);
                    var instanceproc_result = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(suppproc);

                    /////Auto Post of arrival update to sapposting when ETA changed.

                    SAPPosting objSAP = sapPostingRepository.GetDetailsByVCN(obj.VCN);



                    if (objSAP != null)
                    {
                        TextReader tr = new StringReader(objSAP.TransmitData);
                        XDocument xDoc = XDocument.Load(tr);
                        string code = xDoc.Element("ArrivalCreate").Element("CODE").Value;
                        objSAP.CreatedBy = _UserId;
                        objSAP.CreatedDate = DateTime.Now;
                        objSAP.ModifiedBy = _UserId;
                        objSAP.ModifiedDate = DateTime.Now;
                        objSAP.RecordStatus = "A";
                        objSAP.PostingStatus = SAPPostingStatus.New;
                        objSAP.PortCode = _PortCode;
                        objSAP.EmailStatus = "O";
                        objSAP.SMSStatus = "O";
                        objSAP.SystemNotificationStatus = "O";
                        var xmlRes = sapPostingRepository.AutoArrivalUpdateForETAChange(obj.VCN, _PortCode, objSAP.SAPReferenceNo);
                        xmlRes = xmlRes.Replace("#CODE#", code);
                        objSAP.TransmitData = xmlRes;
                        objSAP.ReferenceNo = obj.VCN;
                        objSAP.Remarks = "";
                        objSAP.SAPReferenceNo = objSAP.SAPReferenceNo;
                        objSAP.MessageType = SAPMessageTypes.ArrivalUpdate;
                        objSAP.ObjectState = ObjectState.Added;

                        _unitOfWork.Repository<SAPPosting>().Insert(objSAP);
                    }
                }
                _unitOfWork.SaveChanges();

                // TO DO: Update ETA and ETD in Vessel Call
                notificationpublisher.Publish(entityid, eta.VesselETAChangeID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                return obj;
            });
        }
        #endregion

        #region ChangeETADetails
        /// <summary>
        /// To Get Change ETA Details
        /// </summary>
        /// <returns></returns>
        public List<VesselETAChangeVO> ChangeEtaDetails(string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int agentid = 0;
                if (_UserType == "AGNT")
                {
                    agentid = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId).AgentID;
                }
                if (agentid > 0)
                {
                    return vesseletachangeRepository.ChangeEtaDetailsOnAgentBased(_PortCode, agentid, vcn, vesselName, etaFrom, etaTo, agentNameSearch);
                }
                else
                {
                    return vesseletachangeRepository.ChangeEtaDetails(_PortCode, vcn, vesselName, etaFrom, etaTo, agentNameSearch);
                }
            });
        }
        #endregion

        #region ChangezETADetails
        /// <summary>
        /// To Get Change ETA Details by vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? vesselEatChangeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return vesseletachangeRepository.ChangezEtaDetails(vcn, vesselEatChangeId);
            });
        }
        #endregion

        private string CheckAnyPendingServiceRequest(string vcn, DateTime newEta, DateTime newEtd, DateTime oldEta, DateTime oldEtd, int etachanges, DateTime? planBerth, DateTime? planStartCargo, DateTime? planCompCargo, DateTime? planVacBerth, string voyageIn, string voyageOut)
        {
            string message = string.Empty;

            List<ServiceRequest_VO> pendingServiceRequests =
                (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr =>
                        sr.VCN == vcn && sr.RecordStatus == RecordStatus.Active && sr.IsRecordingCompleted == "N")
                 orderby sr.MovementType, sr.ServiceRequestID
                 select sr).ToList().MapToDto();

            if (pendingServiceRequests.Any())
            {
                foreach (var serviceRequest in pendingServiceRequests)
                {
                    message = serviceRequest.WorkflowInstanceId == null ? "Shifting Notification request is in progress, can't change ETA / ETD." : "Service request is in progress, can't change ETA / ETD.";
                }
            }

            return message;
        }

    }
}
