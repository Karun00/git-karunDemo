using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System;
using System.Data.Entity;
using IPMS.Domain;
using IPMS.Repository;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;
using IPMS.Services.WorkFlow;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VesselCallAnchorageService : ServiceBase, IVesselCallAnchorageService
    {
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private ISAPPostingRepository _sapPostingRepository;
        private INotificationPublisher notificationpublisher;
        private IEntityRepository entityRepository;
        private IUserRepository userRepository;
        private IPortConfigurationRepository portConfigurationRepository;

        private const string _entityCode = EntityCodes.Capture_ArrDeparture;

        public VesselCallAnchorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _sapPostingRepository = new SAPPostingRepository(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public VesselCallAnchorageService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _sapPostingRepository = new SAPPostingRepository(_unitOfWork);
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization            
        }

        public List<VesselCallVO> GetAnchorageRecordingList(string vcn, string vesselName, string etaFrom, string etaTo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                DateTime fromdate = Convert.ToDateTime(etaFrom);
                DateTime todate = Convert.ToDateTime(etaTo).AddDays(1);
                string[] vesselnames = vesselName.Split('-');
                vesselName = vesselnames[0].ToString().Trim();

                var servicedtls = new List<VesselCall>();
                if ((vcn == "ALL") && (vesselName == "ALL"))
                {

                    servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Queryable().Where(a => a.ETA >= fromdate && a.ETA < todate && a.ArrivalNotification.PortCode == _PortCode && a.ArrivalNotification.RecordStatus == RecordStatus.Active)
                                      .Include(t => t.ArrivalNotification)
                                         .Include(t => t.ArrivalNotification.Vessel)
                                          .Include(t => t.ArrivalNotification.VesselCallAnchorages)
                                   orderby t.ModifiedDate descending
                                   select t
                                ).ToList();

                }
                else if ((vcn != "ALL") && (vesselName == "ALL"))
                {

                    servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Queryable().Where(a=> a.ArrivalNotification.PortCode == _PortCode && a.ArrivalNotification.RecordStatus == RecordStatus.Active && (a.VCN.ToUpper().Contains(vcn.Trim().ToUpper())))
                                      .Include(t => t.ArrivalNotification)
                                         .Include(t => t.ArrivalNotification.Vessel)
                                          .Include(t => t.ArrivalNotification.VesselCallAnchorages)//.Select()
                                   orderby t.ModifiedDate descending
                                   select t
                                ).ToList();

                }
                else if ((vcn == "ALL") && (vesselName != "ALL"))
                {

                    servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Queryable().Where(a =>  a.ArrivalNotification.PortCode == _PortCode && a.ArrivalNotification.RecordStatus == RecordStatus.Active
                                      && (a.ArrivalNotification.Vessel.VesselName.ToUpper().Contains(vesselName.Trim().ToUpper())))//.Tracking(true)
                                      .Include(t => t.ArrivalNotification)
                                         .Include(t => t.ArrivalNotification.Vessel)
                                          .Include(t => t.ArrivalNotification.VesselCallAnchorages)//.Select()
                                   orderby t.ModifiedDate descending
                                   select t
                                ).ToList();

                }
                else if ((vcn != "ALL") && (vesselName != "ALL"))
                {

                    servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Queryable().Where(a => a.ArrivalNotification.PortCode == _PortCode && a.ArrivalNotification.RecordStatus == RecordStatus.Active && (a.VCN.ToUpper().Contains(vcn.Trim().ToUpper()))
                                   && ( a.ArrivalNotification.Vessel.VesselName.ToUpper().Contains(vesselName.Trim().ToUpper())))//.Tracking(true)
                                   .Include(t => t.ArrivalNotification)
                                      .Include(t => t.ArrivalNotification.Vessel)
                                       .Include(t => t.ArrivalNotification.VesselCallAnchorages)//.Select()
                                   orderby t.ModifiedDate descending
                                   select t
                             ).ToList();

                }
                else
                {

                    servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Queryable().Where(a => a.ETA >= fromdate && a.ETA < todate && a.ArrivalNotification.PortCode == _PortCode && a.ArrivalNotification.RecordStatus == RecordStatus.Active)//.Tracking(true)
                                      .Include(t => t.ArrivalNotification)
                                         .Include(t => t.ArrivalNotification.Vessel)
                                          .Include(t => t.ArrivalNotification.VesselCallAnchorages) //.Select()
                                   orderby t.ModifiedDate descending
                                   select t
                                ).ToList();

                }

                return servicedtls.MapToDto();

            });
        }

        public List<VesselCallVO> GetzAnchorageRecordingList(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var servicedtls = (from t in _unitOfWork.Repository<VesselCall>().Query()
                                      .Include(t => t.ArrivalNotification.Vessel)
                                       .Include(t => t.ArrivalNotification.VesselCallAnchorages).Select()
                                   orderby t.ModifiedDate descending
                                   select t
                             ).Where(t => t.VCN == vcn).ToList();

                return servicedtls.MapToDto();

            });
        }

        public VesselCallVO ModifyVesselCallAnchorageData(VesselCallVO vesselCallAnchorageData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;
                int UserID = GetUserId(name);
                var entityid = entityRepository.GetEntitiesNotification(EntityCodes.Capture_ArrDeparture).EntityID;
                var portcode = _PortCode;
                var nextStepCompany = userRepository.GetUserDetails(_UserId);

                vesselCallAnchorageData.CreatedBy = UserID;
                vesselCallAnchorageData.CreatedDate = DateTime.Now;
                vesselCallAnchorageData.ModifiedBy = UserID;
                vesselCallAnchorageData.ModifiedDate = DateTime.Now;
                VesselCall obj = new VesselCall();
                obj = VesselCallMapExtenstion.MapToEntity(vesselCallAnchorageData);
                List<VesselCallAnchorage> VesselCallAnchorageslist = obj.ArrivalNotification.VesselCallAnchorages.ToList();
                if (string.IsNullOrEmpty(vesselCallAnchorageData.BreakWaterIn))
                    obj.BreakWaterIn = null;

                if (string.IsNullOrEmpty(vesselCallAnchorageData.BreakWaterOut))
                    obj.BreakWaterOut = null;

                if (string.IsNullOrEmpty(vesselCallAnchorageData.PortLimitIn))
                    obj.PortLimitIn = null;

                if (string.IsNullOrEmpty(vesselCallAnchorageData.PortLimitOut))
                    obj.PortLimitOut = null;
                obj.ATB = vesselCallAnchorageData.ATB.HasValue ? vesselCallAnchorageData.ATB : null;
                obj.ATUB = vesselCallAnchorageData.ATUB.HasValue ? vesselCallAnchorageData.ATUB : null;


                VesselCallVO VesselCallList = new VesselCallVO();

                VesselCallList = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.VCN == vesselCallAnchorageData.VCN)
                                  join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on vc.VCN equals an.VCN
                                  select new VesselCallVO
                                  {
                                      VCN = vc.VCN,
                                      PortLimitIn = vc.PortLimitIn.HasValue ? vc.PortLimitIn.ToString() : string.Empty,
                                      BreakWaterIn = vc.BreakWaterIn.HasValue ? vc.BreakWaterIn.ToString() : string.Empty,
                                      BreakWaterOut = vc.BreakWaterOut.HasValue ? vc.BreakWaterOut.ToString() : string.Empty,
                                      PortLimitOut = vc.PortLimitOut.HasValue ? vc.PortLimitOut.ToString() : string.Empty,
                                      VesselCallAnchorages1 = (from vap in an.VesselCallAnchorages
                                                               select new VesselCallAnchorageVO
                                                               {
                                                                   VesselCallAnchorageID = vap.VesselCallAnchorageID,
                                                                   VCN = vap.VCN,
                                                                   BearingDistanceFromBreakWater = vap.BearingDistanceFromBreakWater,
                                                                   Reason = vap.Reason,
                                                                   AnchorDropTime = vap.AnchorDropTime != null ? vap.AnchorDropTime.ToString() : string.Empty,
                                                                   AnchorAweighTime = vap.AnchorAweighTime.HasValue ? vap.AnchorAweighTime.ToString() : string.Empty,
                                                               }).ToList<VesselCallAnchorageVO>(),
                                  }).FirstOrDefault<VesselCallVO>();

                if (!string.IsNullOrEmpty(vesselCallAnchorageData.BreakWaterIn) && !string.IsNullOrEmpty(vesselCallAnchorageData.BreakWaterOut) &&
                    !string.IsNullOrEmpty(vesselCallAnchorageData.PortLimitIn) && !string.IsNullOrEmpty(vesselCallAnchorageData.PortLimitOut))
                {
                    if (VesselCallList != null)
                    {
                        vesselCallAnchorageData.PortLimitIn = vesselCallAnchorageData.PortLimitIn != ""
                            ? Convert.ToDateTime(vesselCallAnchorageData.PortLimitIn, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        vesselCallAnchorageData.BreakWaterIn = vesselCallAnchorageData.BreakWaterIn != ""
                            ? Convert.ToDateTime(vesselCallAnchorageData.BreakWaterIn, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        vesselCallAnchorageData.BreakWaterOut = vesselCallAnchorageData.BreakWaterOut != ""
                            ? Convert.ToDateTime(vesselCallAnchorageData.BreakWaterOut, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        vesselCallAnchorageData.PortLimitOut = vesselCallAnchorageData.PortLimitOut != ""
                            ? Convert.ToDateTime(vesselCallAnchorageData.PortLimitOut, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;


                        var PortLimitIn = VesselCallList.PortLimitIn != ""
                            ? Convert.ToDateTime(VesselCallList.PortLimitIn, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        var BreakWaterIn = VesselCallList.BreakWaterIn != ""
                            ? Convert.ToDateTime(VesselCallList.BreakWaterIn, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        var BreakWaterOut = VesselCallList.BreakWaterOut != ""
                            ? Convert.ToDateTime(VesselCallList.BreakWaterOut, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;
                        var PortLimitOut = VesselCallList.PortLimitOut != ""
                            ? Convert.ToDateTime(VesselCallList.PortLimitOut, CultureInfo.InvariantCulture)
                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                            : null;

                        if (vesselCallAnchorageData.BreakWaterIn != BreakWaterIn ||
                            vesselCallAnchorageData.BreakWaterOut != BreakWaterOut ||
                            vesselCallAnchorageData.PortLimitIn != PortLimitIn ||
                            vesselCallAnchorageData.PortLimitOut != PortLimitOut)
                        {
                            notificationpublisher.Publish(entityid, obj.VesselCallID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                        }
                    }
                    else
                    {
                        notificationpublisher.Publish(entityid, obj.VesselCallID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                    }

                }
                _unitOfWork.ExecuteSqlCommand("Update VesselCall set ATA = @p0, ATD = @p1, BreakWaterIn = @p2, BreakWaterOut = @p3,PortLimitIn=@p4,PortLimitOut=@p5 , ATB=@p6, ATUB=@p7, ModifiedBy=@p8, ModifiedDate=getdate() where VCN = @p9",
                        obj.ATA, obj.ATD, obj.BreakWaterIn, obj.BreakWaterOut, obj.PortLimitIn, obj.PortLimitOut, obj.ATB, obj.ATUB, _UserId, obj.VCN);

                //////By mahesh : to send arrival update request to SAP when break water in and out.
                if ((obj.BreakWaterIn != null) || (obj.BreakWaterOut != null))
                {
                    SAPPosting objSAP = _sapPostingRepository.GetDetailsByVCN(obj.VCN);
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
                        var xmlRes = _sapPostingRepository.AutoArrivalUpdateForETAChange(obj.VCN, _PortCode, objSAP.SAPReferenceNo);
                        xmlRes = xmlRes.Replace("#CODE#", code);
                        objSAP.TransmitData = xmlRes;
                        objSAP.ReferenceNo = obj.VCN;
                        objSAP.Remarks = "";
                        objSAP.SAPReferenceNo = objSAP.SAPReferenceNo;
                        objSAP.MessageType = SAPMessageTypes.ArrivalUpdate;
                        objSAP.ObjectState = ObjectState.Added;

                        _unitOfWork.Repository<SAPPosting>().Insert(objSAP);
                        _unitOfWork.SaveChanges();
                    }
                }


                if (VesselCallList.VesselCallAnchorages1.Count > 0)
                {
                    foreach (var item in VesselCallList.VesselCallAnchorages1)
                    {
                        _unitOfWork.ExecuteSqlCommand("Update VesselCallAnchorage set RecordStatus = @p0 where VesselCallAnchorageID = @p1",

                         RecordStatus.InActive, item.VesselCallAnchorageID);

                    }

                }
                var i = 0;
                foreach (var VesselCallAnchorages in VesselCallAnchorageslist)
                {
                    var vesselnew = false;
                    VesselCallAnchorages.VCN = vesselCallAnchorageData.VCN;
                    VesselCallAnchorages.CreatedBy = vesselCallAnchorageData.CreatedBy;
                    VesselCallAnchorages.CreatedDate = vesselCallAnchorageData.CreatedDate;
                    VesselCallAnchorages.ModifiedBy = vesselCallAnchorageData.CreatedBy;
                    VesselCallAnchorages.ModifiedDate = vesselCallAnchorageData.CreatedDate;
                    VesselCallAnchorages.RecordStatus = vesselCallAnchorageData.RecordStatus;

                    if (VesselCallAnchorages.VesselCallAnchorageID > 0)
                    {
                        VesselCallAnchorages.ObjectState = ObjectState.Modified;
                        _unitOfWork.Repository<VesselCallAnchorage>().Update(VesselCallAnchorages);
                        vesselnew = false;
                    }
                    else
                    {
                        VesselCallAnchorages.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<VesselCallAnchorage>().Insert(VesselCallAnchorages);
                        vesselnew = true;
                    }


                    var AnchorDropTime = VesselCallAnchorageslist[i].AnchorDropTime != null ? Convert.ToDateTime(VesselCallAnchorageslist[i].AnchorDropTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                    var AnchorAweighTime = VesselCallAnchorageslist[i].AnchorAweighTime != null ? Convert.ToDateTime(VesselCallAnchorageslist[i].AnchorAweighTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                    var Reason = VesselCallAnchorageslist[i].Reason;
                    var BearingDistanceFromBreakWater = VesselCallAnchorageslist[i].BearingDistanceFromBreakWater;

                    if (vesselnew == false)
                    {
                        if (VesselCallAnchorages.AnchorAweighTime != null && VesselCallAnchorages.AnchorDropTime != null)
                        {
                            if (VesselCallList.VesselCallAnchorages1.Count > 0)
                            {
                                foreach (var item in VesselCallList.VesselCallAnchorages1)
                                {
                                    if (item.VesselCallAnchorageID ==
                                        VesselCallAnchorages.VesselCallAnchorageID)
                                    {
                                        var AnchorDropTime1 = item.AnchorDropTime !=
                                                              ""
                                            ? Convert.ToDateTime(
                                                item.AnchorDropTime,
                                                CultureInfo.InvariantCulture)
                                                .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                                            : null;
                                        var AnchorAweighTime1 =
                                            item.AnchorAweighTime != ""
                                                ? Convert.ToDateTime(
                                                    item.AnchorAweighTime,
                                                    CultureInfo.InvariantCulture)
                                                    .ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                                                : null;

                                        if (AnchorDropTime1 != AnchorDropTime || AnchorAweighTime1 != AnchorAweighTime ||
                                            item.Reason != Reason ||
                                            item.BearingDistanceFromBreakWater !=
                                            BearingDistanceFromBreakWater)
                                        {
                                            notificationpublisher.Publish(entityid,
                                                VesselCallAnchorages.VesselCallAnchorageID.ToString(
                                                    CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode,
                                                portConfigurationRepository.GetPortConfiguration(_PortCode).ApproveCode);
                                            vesselnew = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    i++;
                    _unitOfWork.SaveChanges();
                    if (vesselnew == true)
                    {
                        if (VesselCallAnchorages.AnchorAweighTime != null && VesselCallAnchorages.AnchorDropTime != null)
                        {
                            notificationpublisher.Publish(entityid, VesselCallAnchorages.VesselCallAnchorageID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).ApproveCode);
                        }
                    }
                }


                return vesselCallAnchorageData;
            });
        }

        public List<SubCategory> GetReasons()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var GetReasonsType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "ARE").OrderBy(x => x.SubCatName).ToList();
                return GetReasonsType;
            });
        }
        public string GetGeneralConfigs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string ArrivalDepatureConfiguration = _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, BerthPlanningConstants.CaptureArrivalDepature);

                return ArrivalDepatureConfiguration;
            });
        }
        public int GetUserId(string loginName)
        {

            var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                        where u.UserName == loginName
                        select u).FirstOrDefault<User>();

            return user.UserID;
        }
        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> VesselCallVcnDetailsforAutocomplete(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return VesselCallVcnDetailsforAutocomplete(searchvalue, _PortCode);
            });
        }
        /// <summary>
        ///  Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<VesselVO> VesselCallVesselDetailsforAutocomplete(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return VesselCallVesselDetailsforAutocomplete(_PortCode, searchvalue);
            });
        }



        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> VesselCallVcnDetailsforAutocomplete(string searchValue, string portCode)
        {

            var portcode = new SqlParameter("@p_PortCode", portCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var vcndtls = _unitOfWork.SqlQuery<RevenuePostingVO>("dbo.usp_GetVesselCallVCNSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();
            return vcndtls;

        }
        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<VesselVO> VesselCallVesselDetailsforAutocomplete(string PortCode, string searchValue)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetVesselCallVesselSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();
            return _VesselInfo;
        }


        public void VesselCallAnchorageNotification(int vesselCallAnchorageId, string portCode)
        {
            var entitydetails = entityRepository.GetEntitiesNotification(EntityCodes.Capture_ArrDeparture);
            var entityid = entitydetails.EntityID;

            var nextStepCompany = userRepository.GetUserDetails(entitydetails.CreatedBy);

            notificationpublisher.Publish(entityid, vesselCallAnchorageId.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, portCode, portConfigurationRepository.GetPortConfiguration(portCode).ApproveCode);
        }



        public void VesselCallNotification(int portLimitId, string portCode, string vcn)
        {
            var vessel = _unitOfWork.Repository<VesselCall>().Queryable().Where(v => v.VCN == vcn).FirstOrDefault();

            if (vessel != null)
            {
                if (vessel.BreakWaterIn != null && vessel.BreakWaterOut != null && vessel.PortLimitIn != null &&
                    vessel.PortLimitOut != null)
                {
                    var entitydetails = entityRepository.GetEntitiesNotification(EntityCodes.Capture_ArrDeparture);
                    var entityid = entitydetails.EntityID;

                    var nextStepCompany = userRepository.GetUserDetails(entitydetails.CreatedBy);

                    string vesslCallId = Convert.ToString(vessel.VesselCallID);
                    notificationpublisher.Publish(entityid, vesslCallId, _UserId, nextStepCompany, portCode,
                        portConfigurationRepository.GetPortConfiguration(portCode).WorkFlowInitialStatus);
                }
            }
        }



        public VcnCloseVO VcnClose(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                VcnCloseVO vcnDetails = new VcnCloseVO();
                vcnDetails = _unitOfWork.Repository<VesselCall>().Queryable().Where(x => x.VCN == vcn).Select(x => new VcnCloseVO
                {
                 VesselCallID=x.VesselCallID,
                 VCN=x.VCN,
                 RecentAgentID=x.RecentAgentID,
                 ETA = x.ETA,
                 ETD = x.ETD,
                 ETB = x.ETB,
                 ETUB = x.ETUB,
                 ATA = x.ATA,
                 ATD = x.ATD,
                 ATB = x.ATB,
                 ATUB = x.ATUB,
                 BreakWaterIn = x.BreakWaterIn,
                 BreakWaterOut = x.BreakWaterOut,
                 PortLimitIn = x.PortLimitIn,
                 PortLimitOut = x.PortLimitOut,
                 AnchorUp = x.AnchorUp,
                 AnchorDown = x.AnchorDown,
                 FromPositionPortCode = x.FromPositionPortCode,
                 FromPositionQuayCode = x.FromPositionQuayCode,
                 FromPositionBerthCode = x.FromPositionBerthCode,
                 FromPositionBollardCode = x.FromPositionBollardCode,
                 ToPositionPortCode = x.ToPositionPortCode,
                 ToPositionQuayCode = x.ToPositionQuayCode,
                 ToPositionBerthCode = x.ToPositionBerthCode,
                 ToPositionBollardCode = x.ToPositionBollardCode,
                 RecordStatus = x.RecordStatus,
                 CreatedBy = x.CreatedBy,
                 CreatedDate = x.CreatedDate,
                 ModifiedBy = x.ModifiedBy,
                 ModifiedDate = x.ModifiedDate,
                 NoofTimesETAChanged = x.NoofTimesETAChanged,

                }).FirstOrDefault<VcnCloseVO>();

                TimeSpan thirtyMin = TimeSpan.FromMinutes(30);
                TimeSpan oneHour = TimeSpan.FromMinutes(60);

                if (vcnDetails != null)
                {

                    if (!vcnDetails.PortLimitIn.HasValue && !vcnDetails.BreakWaterIn.HasValue && !vcnDetails.ATA.HasValue && !vcnDetails.ATB.HasValue && vcnDetails.ETB.HasValue)
                    {
                        vcnDetails.ATB = Convert.ToDateTime(vcnDetails.ETB);
                        vcnDetails.ATA = !vcnDetails.ATA.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.ATA;
                        vcnDetails.BreakWaterIn = !vcnDetails.BreakWaterIn.HasValue ? vcnDetails.ATB - thirtyMin : vcnDetails.BreakWaterIn;
                        vcnDetails.PortLimitIn = !vcnDetails.PortLimitIn.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.PortLimitIn;
                    }
                    else if (vcnDetails.PortLimitIn.HasValue || vcnDetails.BreakWaterIn.HasValue || vcnDetails.ATA.HasValue || vcnDetails.ATB.HasValue)
                    {
                        if (vcnDetails.PortLimitIn.HasValue)
                        {
                            vcnDetails.BreakWaterIn = !vcnDetails.BreakWaterIn.HasValue ? vcnDetails.PortLimitIn + thirtyMin : vcnDetails.BreakWaterIn;
                            vcnDetails.ATB = !vcnDetails.ATB.HasValue ? vcnDetails.BreakWaterIn + thirtyMin : vcnDetails.ATB;
                            vcnDetails.ATA = !vcnDetails.ATA.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.ATA;
                        }
                        else if (vcnDetails.BreakWaterIn.HasValue)
                        {
                            vcnDetails.ATB = !vcnDetails.ATB.HasValue ? vcnDetails.BreakWaterIn + thirtyMin : vcnDetails.ATB;
                            vcnDetails.ATA = !vcnDetails.ATA.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.ATA;
                            vcnDetails.PortLimitIn = !vcnDetails.PortLimitIn.HasValue ? vcnDetails.BreakWaterIn - thirtyMin : vcnDetails.PortLimitIn;
                        }
                        else if (vcnDetails.ATA.HasValue)
                        {
                            vcnDetails.ATB = !vcnDetails.ATB.HasValue ? vcnDetails.ATA + oneHour : vcnDetails.ATB;
                            vcnDetails.BreakWaterIn = !vcnDetails.BreakWaterIn.HasValue ? vcnDetails.ATB - thirtyMin : vcnDetails.BreakWaterIn;
                            vcnDetails.PortLimitIn = !vcnDetails.PortLimitIn.HasValue ? vcnDetails.BreakWaterIn - thirtyMin : vcnDetails.PortLimitIn;
                        }
                        else if (vcnDetails.ATB.HasValue)
                        {
                            vcnDetails.ATA = !vcnDetails.ATA.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.ATA;
                            vcnDetails.BreakWaterIn = !vcnDetails.BreakWaterIn.HasValue ? vcnDetails.ATB - thirtyMin : vcnDetails.BreakWaterIn;
                            vcnDetails.PortLimitIn = !vcnDetails.PortLimitIn.HasValue ? vcnDetails.ATB - oneHour : vcnDetails.PortLimitIn;
                        }
                    }

                    if (!vcnDetails.PortLimitOut.HasValue && !vcnDetails.BreakWaterOut.HasValue && !vcnDetails.ATUB.HasValue && !vcnDetails.ATD.HasValue && vcnDetails.ETD != null )
                    {
                        vcnDetails.ATD = Convert.ToDateTime(vcnDetails.ETD);
                        vcnDetails.ATUB = vcnDetails.ATUB == null || !vcnDetails.ATUB.HasValue ? vcnDetails.ATD - thirtyMin : vcnDetails.ATUB;
                        vcnDetails.BreakWaterOut = vcnDetails.BreakWaterOut == null || !vcnDetails.BreakWaterOut.HasValue ? vcnDetails.ATD : vcnDetails.BreakWaterOut;
                        vcnDetails.PortLimitOut = vcnDetails.PortLimitOut == null || !vcnDetails.PortLimitOut.HasValue ? vcnDetails.ATD + thirtyMin : vcnDetails.PortLimitOut;
                    }
                    else if (vcnDetails.PortLimitOut.HasValue || vcnDetails.BreakWaterOut.HasValue || vcnDetails.ATUB.HasValue || vcnDetails.ATD.HasValue)
                    {
                        if (vcnDetails.PortLimitOut.HasValue)
                        {
                            vcnDetails.BreakWaterOut = !vcnDetails.BreakWaterOut.HasValue ? vcnDetails.PortLimitOut - thirtyMin : vcnDetails.BreakWaterOut;
                            vcnDetails.ATD = !vcnDetails.ATD.HasValue ? vcnDetails.BreakWaterOut : vcnDetails.ATD;
                            vcnDetails.ATUB = !vcnDetails.ATUB.HasValue ? vcnDetails.ATD - thirtyMin : vcnDetails.ATUB;
                        }
                        else if (vcnDetails.BreakWaterOut.HasValue)
                        {
                            vcnDetails.ATD = !vcnDetails.ATD.HasValue ? vcnDetails.BreakWaterOut : vcnDetails.ATD;
                            vcnDetails.ATUB = !vcnDetails.ATUB.HasValue ? vcnDetails.ATD - thirtyMin : vcnDetails.ATUB;
                            vcnDetails.PortLimitOut = !vcnDetails.PortLimitOut.HasValue ? vcnDetails.BreakWaterOut + thirtyMin : vcnDetails.PortLimitOut;
                        }
                        else if (vcnDetails.ATUB.HasValue)
                        {
                            vcnDetails.ATD = !vcnDetails.ATD.HasValue ? vcnDetails.ATUB + thirtyMin : vcnDetails.ATD;
                            vcnDetails.BreakWaterOut = !vcnDetails.BreakWaterOut.HasValue ? vcnDetails.ATD : vcnDetails.BreakWaterOut;
                            vcnDetails.PortLimitOut = !vcnDetails.PortLimitOut.HasValue ? vcnDetails.BreakWaterOut + thirtyMin : vcnDetails.PortLimitOut;

                        }
                        else if (vcnDetails.ATD.HasValue)
                        {
                            vcnDetails.ATUB = !vcnDetails.ATUB.HasValue ? vcnDetails.ATD - thirtyMin : vcnDetails.ATUB;
                            vcnDetails.BreakWaterOut = !vcnDetails.BreakWaterOut.HasValue ? vcnDetails.ATD : vcnDetails.BreakWaterOut;
                            vcnDetails.PortLimitOut = !vcnDetails.PortLimitOut.HasValue ? vcnDetails.ATD + thirtyMin : vcnDetails.PortLimitOut;
                        }
                    }


                    if (vcnDetails.ATA > vcnDetails.ATD)
                    {
                        vcnDetails.ATB = vcnDetails.ATA + oneHour;
                        vcnDetails.PortLimitIn = vcnDetails.ATA;
                        vcnDetails.BreakWaterIn = vcnDetails.ATA + thirtyMin;
                        vcnDetails.BreakWaterOut=null;
                        vcnDetails.PortLimitOut = null;
                        vcnDetails.ATD = null;
                    }

                }
                return vcnDetails.MapToDto();
            });
        }


    }
}
