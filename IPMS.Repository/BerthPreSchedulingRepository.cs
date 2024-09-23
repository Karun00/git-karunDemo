using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.Entity;
using System.Linq;


namespace IPMS.Repository
{
    public class BerthPreSchedulingRepository : IBerthPreSchedulingRepository
    {
        private IUnitOfWork _unitOfWork;
        private IBerthPlanningRepository _berthPlanningRepository;
        public BerthPreSchedulingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _berthPlanningRepository = new BerthPlanningRepository(_unitOfWork);
        }


        public List<AgentData> GetAllAgents(string portCode)
        {
            List<AgentData> agents = (from a in _unitOfWork.Repository<Agent>().Queryable()
                                      .Include(a => a.AgentPorts).Select(a => a).Where(a => a.AgentPorts.Any(p => p.PortCode == portCode && p.WFStatus == "WFSA"))
                                      where a.RecordStatus == "A"
                                      select new AgentData
                                      {
                                          AgentID = a.AgentID,
                                          RegisteredName = a.RegisteredName
                                      }).OrderBy(x => x.RegisteredName).ToList();
            return agents;
        }

        public List<VCMData> GetVCMList(string portCode, int userId, string userType, string agentId, string eta, string etd, string vesselType, string reasonForVisit, string cargoType, string movementStatus)
        {
            List<VCMData> _VCMList = new List<VCMData>();
            int UserTypeID = 0;
            if (userType == "TO")
            {
                UserTypeID = (from u in _unitOfWork.Repository<User>().Queryable().Select(u => u)
                              where u.UserID == userId
                              select u.UserTypeID).FirstOrDefault<int>();
               
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", UserTypeID);
                var _eta = new SqlParameter("@ETA", DateTime.Parse(eta, CultureInfo.InvariantCulture));
                var _etd = new SqlParameter("@ETD", DateTime.Parse(etd, CultureInfo.InvariantCulture));

                _VCMList = _unitOfWork.SqlQuery<VCMData>("usp_GetVCMList @PortCode,@UserType,@UserID,@ETA,@ETD", _portCode, _userType, _userID, _eta, _etd).ToList();
            }
            else
            {
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", userId);
                var _eta = new SqlParameter("@ETA", DateTime.Parse(eta, CultureInfo.InvariantCulture));
                var _etd = new SqlParameter("@ETD", DateTime.Parse(etd, CultureInfo.InvariantCulture));

                _VCMList = _unitOfWork.SqlQuery<VCMData>("usp_GetVCMList @PortCode,@UserType,@UserID,@ETA,@ETD", _portCode, _userType, _userID, _eta, _etd).ToList();
            }

            if (agentId != "All")
                _VCMList = _VCMList.Where(vcm => vcm.AgentID == Convert.ToInt32(agentId, CultureInfo.InvariantCulture)).ToList();

            if (vesselType != "All")
                _VCMList = _VCMList.Where(vcm => vcm.VesselTypeCode == vesselType).ToList();

            if (reasonForVisit != "All")
                _VCMList = _VCMList.Where(vcm => vcm.ArrivalReasons.Contains(reasonForVisit)).ToList();

            if (cargoType != "All")
                _VCMList = _VCMList.Where(vcm => vcm.CargoType.Contains(cargoType)).ToList();

            if (movementStatus != "All")
                _VCMList = _VCMList.Where(vcm => vcm.MovementStatus == movementStatus).ToList();

            return _VCMList;
        }

        /// <summary>
        /// To Get VCM List For Berth Planning Table View
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<VCMTableData> GetVCMTableList(string portCode, int userId, string userType, string quayCode, string berthCode, string vesselStatus, string eta, string toDate)
        {
            CultureInfo Culture = new CultureInfo("en-US");
            List<VCMTableData> _VCMTableList = new List<VCMTableData>();           

            var bpcdata = new List<BerthPlanningConfiguration>();
            bpcdata = _berthPlanningRepository.GetBerthPlanningConfigurations(portCode);

            var berths = (from b in _unitOfWork.Repository<Berth>().Queryable().Where(b => b.PortCode == portCode) select b).ToList();
            var cargoTypes = (from ct in _unitOfWork.Repository<SubCategory>().Queryable().Where(ct => ct.SupCatCode == SuperCategoryConstants.CARGO_TYPE) select ct).ToList();
            var arrivalReasons = (from ar in _unitOfWork.Repository<SubCategory>().Queryable().Where(ar => ar.SupCatCode == SuperCategoryConstants.REASON_FOR_VISIT_TYPE) select ar).ToList();

            var scheduleColor = Convert.ToString((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.ScheduleColor))).ConfigValue.Trim(), CultureInfo.InvariantCulture);
            var confirmColor = Convert.ToString((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.ConfirmColor))).ConfigValue.Trim(), CultureInfo.InvariantCulture);
            var berthedColor = Convert.ToString((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.BerthedColor))).ConfigValue.Trim(), CultureInfo.InvariantCulture);
            var sailedColor = Convert.ToString((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.SailedColor))).ConfigValue.Trim(), CultureInfo.InvariantCulture);
            var vesselArrestedColor = Convert.ToString((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.ArrestedColor))).ConfigValue.Trim(), CultureInfo.InvariantCulture);
            if (userType == "TO")
            {
                int UserTypeID = _unitOfWork.Repository<User>().Find(userId).UserTypeID;

                _VCMTableList = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable()
                                     .Where(a => a.ArrivalNotification.PortCode == portCode && a.ArrivalNotification.TerminalOperatorID == UserTypeID
                                         && a.RecordStatus == RecordStatus.Active && a.MovementStatus != "MPEN").OrderBy(a => a.ArrivalNotification.ETA)
                                 select new VCMTableData
                                 {
                                     VCN = vcm.VCN,
                                     VesselName = vcm.ArrivalNotification.Vessel.VesselName,
                                     CargoType = vcm.ArrivalNotification.ArrivalCommodities.Where(bc => bc.RecordStatus == "A").Select(bc => bc.CargoType).ToList<String>(),
                                     LengthOverallInM = vcm.ArrivalNotification.Vessel.LengthOverallInM,
                                     ETB = (DateTime?)vcm.ETB,
                                     ETUB = (DateTime?)vcm.ETUB,
                                     ATB = vcm.ATB,
                                     ATUB = vcm.ATUB,
                                     IMDG = vcm.ArrivalNotification.AnyDangerousGoodsonBoard == "I" ? "No" : "Yes",
                                     BerthTime = (vcm.ATB == null ? vcm.ETB : vcm.ATB) ?? DateTime.MinValue,
                                     UnBerthTime = (vcm.ATUB == null ? vcm.ETUB : vcm.ATUB) ?? DateTime.MinValue,
                                     QuayCode = vcm.FromPositionQuayCode,
                                     BerthCode = vcm.FromPositionBerthCode,
                                     FromBollard = vcm.FromPositionBollardCode == null ? "" : vcm.Bollard.BollardName,
                                     ToBollard = vcm.ToPositionBollardCode == null ? "" : vcm.Bollard1.BollardName,
                                     MovementType = vcm.MovementType,
                                     MovementStatus = vcm.MovementStatus,
                                     VesselArrested = vcm.ArrivalNotification.VesselArrestImmobilizationSAMSAs.Where(vc => vc.VesselArrested == "Y" && vc.VesselReleased == "N").Select(vc => vc.VCN).ToList<String>(),
                                     ArrivalReasons = vcm.ArrivalNotification.ArrivalReasons.Where(ar => ar.RecordStatus == "A").Select(ar => ar.Reason).ToList<String>(),
                                     MooringBowBollard = vcm.MooringBollardBowBollardCode != null ? vcm.Bollard2.BollardName : "",
                                     MooringStemBollard = vcm.MooringBollardStemBollardCode != null ? vcm.Bollard3.BollardName : "",
                                     CargoTypeName = "",
                                     ReasonForVisitName = ""
                                 }).ToList();
            }
            else
            {
                _VCMTableList = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable()
                                     .Where(a => a.ArrivalNotification.PortCode == portCode && a.RecordStatus == RecordStatus.Active 
                                         && (a.MovementType == "ARMV" || a.MovementType == "SHMV") && a.MovementStatus != "MPEN")
                                         .OrderBy(a => a.ArrivalNotification.ETA)
                                 select new VCMTableData
                                 {
                                     VCN = vcm.VCN,
                                     VesselName = vcm.ArrivalNotification.Vessel.VesselName,
                                     CargoType = vcm.ArrivalNotification.ArrivalCommodities.Where(bc => bc.RecordStatus == "A").Select(bc => bc.CargoType).ToList<String>(),
                                     LengthOverallInM = vcm.ArrivalNotification.Vessel.LengthOverallInM,
                                     ETB = (DateTime?)vcm.ETB,
                                     ETUB = (DateTime?)vcm.ETUB,
                                     ATB = vcm.ATB,
                                     ATUB = vcm.ATUB,
                                     IMDG = vcm.ArrivalNotification.AnyDangerousGoodsonBoard == "I" ? "No" : "Yes",
                                     BerthTime = (vcm.ATB == null ? vcm.ETB : vcm.ATB) ?? DateTime.MinValue,
                                     UnBerthTime = (vcm.ATUB == null ? vcm.ETUB : vcm.ATUB) ?? DateTime.MinValue,
                                     QuayCode = vcm.FromPositionQuayCode,
                                     BerthCode = vcm.FromPositionBerthCode,
                                     FromBollard = vcm.FromPositionBollardCode == null ? "" : vcm.Bollard.BollardName,
                                     ToBollard = vcm.ToPositionBollardCode == null ? "" : vcm.Bollard1.BollardName,
                                     MovementType = vcm.MovementType,
                                     MovementStatus = vcm.MovementStatus,
                                     VesselArrested = vcm.ArrivalNotification.VesselArrestImmobilizationSAMSAs.Where(vc => vc.VesselArrested == "Y" && vc.VesselReleased == "N").Select(vc => vc.VCN).ToList<String>(),
                                     ArrivalReasons = vcm.ArrivalNotification.ArrivalReasons.Where(ar => ar.RecordStatus == "A").Select(ar => ar.Reason).ToList<String>(),
                                     MooringBowBollard = vcm.MooringBollardBowBollardCode != null ? vcm.Bollard2.BollardName : "",
                                     MooringStemBollard = vcm.MooringBollardStemBollardCode != null ? vcm.Bollard3.BollardName : "",
                                     CargoTypeName = "",
                                     ReasonForVisitName = ""
                                 }).ToList();
            }

            _VCMTableList = _VCMTableList.Where(vcm => ((DateTime.Parse(eta, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(eta, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toDate, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toDate, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();

            if (quayCode != "undefined")
                _VCMTableList = _VCMTableList.Where(vcm => vcm.QuayCode == quayCode).ToList();

            if (berthCode != "undefined")
                _VCMTableList = _VCMTableList.Where(vcm => vcm.BerthCode == berthCode).ToList();

            if (vesselStatus != "undefined")
                _VCMTableList = _VCMTableList.Where(vcm => vcm.MovementStatus == vesselStatus).ToList();

            (from vcm in _VCMTableList select vcm).ToList().ForEach((vcm) =>
            {
                string subCatName = string.Empty;

                vcm.isVesselArrested = vcm.VesselArrested.Count > 0 ? true : false;
                               
                (from cargotype in vcm.CargoType select cargotype).ToList().ForEach((cargotype) =>
                {
                    subCatName = cargoTypes.Find(s => s.SubCatCode == cargotype).SubCatName;

                    vcm.CargoTypeName = (String.IsNullOrEmpty(vcm.CargoTypeName)) ? subCatName : vcm.CargoTypeName + ", " + subCatName;
                    subCatName = string.Empty;
                });

                (from ar in vcm.ArrivalReasons select ar).ToList().ForEach((ar) =>
                {
                    subCatName = arrivalReasons.Find(s => s.SubCatCode == ar).SubCatName;

                    vcm.ReasonForVisitName = (String.IsNullOrEmpty(vcm.ReasonForVisitName)) ? subCatName : vcm.ReasonForVisitName + ", " + subCatName;
                    subCatName = string.Empty;
                });

                if (vcm.isVesselArrested == true)
                {
                    vcm.VesselColor = vesselArrestedColor;
                }
                else
                {
                    if (vcm.MovementType == MovementTypes.SAILING)
                        vcm.VesselColor = sailedColor;
                    else if (vcm.MovementStatus == MovementStatus.SCHEDULED)
                        vcm.VesselColor = scheduleColor;
                    else if (vcm.MovementStatus == MovementStatus.CONFIRMED)
                        vcm.VesselColor = confirmColor;
                    else if (vcm.MovementStatus == MovementStatus.BERTHED)
                        vcm.VesselColor = berthedColor;
                    else if (vcm.MovementStatus == MovementStatus.SAILED)
                        vcm.VesselColor = sailedColor;
                }
                // Vessel Color

                vcm.Berth = vcm.BerthCode != null ? berths.Find(b => b.PortCode == portCode && b.QuayCode == vcm.QuayCode && b.BerthCode == vcm.BerthCode).BerthName : "";

            });

            return _VCMTableList;
        }

        public List<BerthDataVO> GetBerthsList(string portCode)
        {
            CultureInfo Culture = new CultureInfo("en-US");
            var Berth = (from berth in _unitOfWork.Repository<Berth>().Query()
                              .Include(berth => berth.BerthCargoes)
                              .Include(berth => berth.Bollards)
                              .Include(berth => berth.BerthVesselTypes)
                              .Include(berth => berth.BerthReasonForVisits)
                              .Include(b => b.TerminalOperatorBerths)
                             .Select()
                         where berth.PortCode == portCode && berth.BerthType != DryDockStatus.DryDockType
                         select new BerthDataVO
                         {
                             PortCode = berth.PortCode,
                             QuayCode = berth.QuayCode,
                             BerthCode = berth.BerthCode,
                             BerthName = berth.BerthName,
                             ShortName = berth.ShortName,
                             BerthType = berth.BerthType,
                             Lengthm = berth.Lengthm,
                             Draftm = berth.Draftm,
                             TidalDraft = Convert.ToDecimal(berth.TidalDraft, Culture),
                             BerthCargoes = berth.BerthCargoes.Where(bc => bc.RecordStatus == "A").ToList(),
                             BerthVesselTypes = berth.BerthVesselTypes.ToList(),
                             BerthReasonForVisits = berth.BerthReasonForVisits.ToList(),
                             TerminalOperators = (berth.TerminalOperatorBerths.Select(to => to.TerminalOperatorID)).ToList<int>(),
                             Bollards = berth.Bollards.Select(bo => new BollardVO
                             {
                                 PortCode = bo.PortCode,
                                 QuayCode = bo.QuayCode,
                                 BerthCode = bo.BerthCode,
                                 BollardName = bo.BollardName,
                                 BollardCode = bo.BollardCode,
                                 FromMeter = bo.FromMeter,
                                 ToMeter = bo.ToMeter,
                                 Continous = bo.Continuous
                             }).OrderBy(bo => bo.FromMeter).ToList()
                         }).ToList();

            return Berth;
        }
               
        public List<SuitableBerthVO> GetSuitableBerths(string vcn, string portCode, int userId, string userType, string etb, string etub, string vesselCallMovementId)
        {
            CultureInfo Culture = new CultureInfo("en-US");
            int UserTypeID = 0;

            List<SuitableBerthVO> _SuitableAvailableBerthList = new List<SuitableBerthVO>();
            List<SuitableBerthVO> _SuitableBerthList = new List<SuitableBerthVO>();
            List<BerthDataVO> _BerthList = new List<BerthDataVO>();
            VesselData VesselInformation = new VesselData();
            List<BerthPlanningConfiguration> bpcdata = new List<BerthPlanningConfiguration>();

            // Get UnderKeelClearance 
            bpcdata = _berthPlanningRepository.GetBerthPlanningConfigurations(portCode);
            decimal UnderKneelClearance = Convert.ToDecimal((bpcdata.Find(x => x.ConfigName.Contains(BerthPlanningConstants.UnderKeelClearance)).ConfigValue.Trim()).Replace(',', '.'), Culture);

            if (userType == "TO")
            {
                UserTypeID = (from u in _unitOfWork.Repository<User>().Queryable()
                              where u.UserID == userId
                              select u.UserTypeID).FirstOrDefault<int>();
            }

            // Get all Berths of the Port
            _BerthList = GetBerthsList(portCode);

            int vesselCallMovementIdConverted = Convert.ToInt32(vesselCallMovementId);
            // Get Vessel Information 
            VesselInformation = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query(v => v.VCN == vcn && v.VesselCallMovementID == vesselCallMovementIdConverted).
                            Include(vcm => vcm.ArrivalNotification).
                            Include(vcm => vcm.ArrivalNotification.Agent).
                            Include(vcm => vcm.ArrivalNotification.Vessel).
                            Include(vcm => vcm.ArrivalNotification.Vessel.SubCategory3).
                            Include(vcm => vcm.ArrivalNotification.Berth).
                            Include(vcm => vcm.ArrivalNotification.Berth1).
                            Include(vcm => vcm.ArrivalNotification.ArrivalCommodities).
                            Include(vcm => vcm.ArrivalNotification.ArrivalReasons).
                            Include(vcm => vcm.ServiceRequest).
                            Select()
                                 select new VesselData
                                 {
                                     VCN = vcm.VCN,
                                     VesselName = vcm.ArrivalNotification.Vessel.VesselName,
                                     VesselType = vcm.ArrivalNotification.Vessel.SubCategory3.SubCatName,
                                     VesselTypeCode = vcm.ArrivalNotification.Vessel.VesselType,
                                     LengthOverallInM = vcm.ArrivalNotification.Vessel.LengthOverallInM,                                     
                                     ArrDraft = vcm.MovementType != "SHMV" ? (Convert.ToDecimal((vcm.ArrivalNotification.ArrDraft.Trim()).Replace(',', '.'), Culture)) : Convert.ToDecimal(vcm.ServiceRequest.DraftAFT),
                                     DeptDraft = vcm.MovementType != "SHMV" ? (Convert.ToDecimal((vcm.ArrivalNotification.DepDraft.Trim()).Replace(',', '.'), Culture)) : Convert.ToDecimal(vcm.ServiceRequest.DraftFWD),
                                     PreferredBerth = vcm.ArrivalNotification.Berth1.BerthName,
                                     AlternateBerth = vcm.ArrivalNotification.AlternateBerthCode == null ? "" : vcm.ArrivalNotification.Berth.BerthName,
                                     ArrivalReasons = vcm.ArrivalNotification.ArrivalReasons.Where(ar => ar.RecordStatus == "A").Select(ar => ar.Reason).ToList<String>(),                                     
                                     Tidal = vcm.MovementType == MovementTypes.ARRIVAL ? vcm.ArrivalNotification.Tidal : (vcm.ServiceRequest.IsTidal == "Y" ? "A" : "I"),
                                     BerthCode = vcm.FromPositionBerthCode,
                                     ETB = vcm.ETB,
                                     ETUB = vcm.ETUB,
                                     ArrivalCommodities = vcm.ArrivalNotification.ArrivalCommodities.Where(ac => ac.RecordStatus == "A").Select(ac => ac.CargoType).ToList<String>()
                                 }).FirstOrDefault();

            foreach (BerthDataVO berth in _BerthList)
            {
                Boolean draftRule = false;
                Boolean cargoTypeRule = false;
                Boolean terminalBerthRule = false;

                // Terminal Operator Berth 
                if (userType == "TO")
                {
                    terminalBerthRule = berth.TerminalOperators.Contains(UserTypeID);
                }
                else
                    terminalBerthRule = true;

                // Draft rule 
                if (VesselInformation.Tidal == "A")
                {
                    draftRule = true;
                }
                else
                {
                    if ((VesselInformation.ArrDraft + UnderKneelClearance) <= berth.Draftm)
                    {
                        draftRule = true;
                    }
                }

                // Cargo Type Rule                
                if (VesselInformation.ArrivalReasons.Contains("BUNK") || VesselInformation.ArrivalReasons.Contains("LABY") || VesselInformation.ArrivalReasons.Contains("REPA") || VesselInformation.ArrivalReasons.Contains("STOR") || VesselInformation.ArrivalReasons.Contains("DRYD"))
                {
                    cargoTypeRule = true;
                }
                else
                {
                    foreach (var cargotype in VesselInformation.ArrivalCommodities)
                    {
                        foreach (var bert in berth.BerthCargoes)
                        {
                            if (bert.CargoTypeCode == cargotype)
                                cargoTypeRule = true;
                        }
                    }
                }
                                
                if (draftRule == true && cargoTypeRule == true && terminalBerthRule == true)
                {
                    SuitableBerthVO _SuitableBerth = new SuitableBerthVO
                    {
                        BerthCode = berth.BerthCode,
                        BerthName = berth.BerthName,
                        Draft = berth.Draftm,
                        Length = berth.Lengthm,
                        PortCode = berth.PortCode,
                        QuayCode = berth.QuayCode,
                        Bollards = berth.Bollards,
                        LengthOverallInM = VesselInformation.LengthOverallInM

                    };
                    _SuitableBerthList.Add(_SuitableBerth);
                }
            }

            foreach (var berth in _SuitableBerthList)
            {
                SuitableBerthVO _SuitableAvailBerth = new SuitableBerthVO
                {
                    BerthCode = berth.BerthCode,
                    BerthName = berth.BerthName,
                    Draft = berth.Draftm,
                    Length = berth.Lengthm,
                    PortCode = berth.PortCode,
                    QuayCode = berth.QuayCode,
                    Bollards = berth.Bollards,
                    LengthOverallInM = VesselInformation.LengthOverallInM

                };
                _SuitableAvailableBerthList.Add(_SuitableAvailBerth);
            }

            return _SuitableAvailableBerthList;
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int userId)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userId
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }

        /// <summary>
        /// To Get Vessel Call Moments By VesselCallMovementID
        /// </summary>
        /// <param name="vesselCallMovementId"></param>
        /// <returns></returns>
        public VesselCallMovement GetVesselCallMomentDetailsById(string vesselCallMovementId)
        {

            var vesselrequest = (from v in _unitOfWork.Repository<VesselCallMovement>().Query()
                                .Tracking(true)
                                .Include(vcm => vcm.ArrivalNotification.Vessel)
                                .Include(vcm => vcm.MovementType_SubCategory)
                                .Include(vcm => vcm.MovementStatusName)
                                .Include(vcm => vcm.Bollard.Berth)
                                .Include(vcm => vcm.Bollard)
                                .Include(vcm => vcm.Bollard1)
                                  .Select()
                                 where v.VesselCallMovementID == Convert.ToInt32(vesselCallMovementId, CultureInfo.InvariantCulture)
                                 select new VesselCallMovement
                                 {
                                     VesselCallMovementID = v.VesselCallMovementID,
                                     VCN = v.VCN,
                                     VesselName = v.ArrivalNotification.Vessel.VesselName,
                                     MovementName = v.MovementStatusName.SubCatName,
                                     MovementTypeName = v.MovementType_SubCategory.SubCatName,
                                     ETB = v.ETB,
                                     ETUB = v.ETUB,
                                     BerthName = v.Bollard.Berth.BerthName,
                                     BollardFrom = v.Bollard.BollardName,
                                     BollardTo = v.Bollard1.BollardName
                                 }).FirstOrDefault<VesselCallMovement>();

            return vesselrequest;
        }

    }
}
