using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.Data.Entity;

namespace IPMS.Repository
{
    public class BerthPlanningRepository : IBerthPlanningRepository
    {

        private IUnitOfWork _unitOfWork;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository;
        private IServiceRequestRepository _serviceRequestRepository;

        public BerthPlanningRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
        }

        /// <summary>
        /// To Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<QuayVO> GetQuaysInPort(string portCode)
        {
            List<QuayVO> quays = (from a in _unitOfWork.Repository<Quay>().Queryable()
                                  .Include(b => b.Port)
                                  where (a.PortCode == portCode)
                                  select new QuayVO
                                  {
                                      PortCode = a.PortCode,
                                      PortName = a.Port.PortName,
                                      QuayCode = a.QuayCode,
                                      QuayName = a.QuayName,
                                      QuayLength = a.QuayLength
                                  }).ToList();
            return quays;
        }

        /// <summary>
        /// To Get BerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthsData> GetBerthsWithBollard(string portCode, string quayCode)
        {
            List<BerthsData> berthswithbollards = new List<BerthsData>();
            List<QuayBerthBollardData> quaysberthbollards = new List<QuayBerthBollardData>();
            List<QuayVO> quays = (from a in _unitOfWork.Repository<Quay>().Query().Select()
                                  where (a.PortCode == portCode)
                                  select new QuayVO
                                  {
                                      PortCode = a.PortCode,
                                      QuayCode = a.QuayCode,
                                      QuayName = a.QuayName,
                                      QuayLength = a.QuayLength
                                  }).ToList();

            berthswithbollards = GetBerthsWithBollard_BerthsWithBollards(portCode, berthswithbollards);

            foreach (QuayVO quay in quays)
            {
                QuayBerthBollardData qbb = new QuayBerthBollardData();
                qbb.QuayCode = quay.QuayCode;
                qbb.QuayLength = quay.QuayLength;
                qbb.QuayName = quay.QuayName;
                qbb.Berths = berthswithbollards.Where(p => p.QuayCode == quay.QuayCode).ToList();
                quaysberthbollards.Add(qbb);
            }

            return berthswithbollards;
        }

        private List<BerthsData> GetBerthsWithBollard_BerthsWithBollards(string portCode, List<BerthsData> berthsWithBollards)
        {
            berthsWithBollards = (from q in
                                      (from b in _unitOfWork.Repository<Berth>().Query()
                                                   .Include(b => b.Quay)
                                                   .Include(b => b.BerthCargoes)
                                                   .Include(b => b.BerthVesselTypes)
                                                   .Include(b => b.BerthReasonForVisits)
                                                   .Include(b => b.TerminalOperatorBerths)
                                                   .Include(b => b.Bollards).Select()
                                                   .Where(b => b.PortCode == portCode
                                                     && b.Bollards.Count > 0)
                                       select new
                                       {
                                           PortCode = b.PortCode,
                                           QuayCode = b.QuayCode,
                                           QuayLength = b.Quay.QuayLength,
                                           BerthCode = b.BerthCode,
                                           BerthName = b.BerthName,
                                           TidalDraft = b.TidalDraft,
                                           Min = b.Bollards.Min(bo => bo.FromMeter),
                                           Lengthm = b.Lengthm,
                                           Draftm = b.Draftm,
                                           BerthType = b.BerthType,
                                           CargoType = (b.BerthCargoes.Select(bc => bc.CargoTypeCode)).ToList<String>(),
                                           VesselTypes = (b.BerthVesselTypes.Select(bvt => bvt.VesselTypeCode)).ToList<String>(),
                                           ReasonsForVisitType = (b.BerthReasonForVisits.Select(rfv => rfv.ReasonForVisitCode)).ToList<String>(),
                                           TerminalOperators = (b.TerminalOperatorBerths.Select(to => to.TerminalOperatorID)).ToList<int>(),
                                           Bollards = b.Bollards.Select(bo => new
                                           {
                                               PortCode = bo.PortCode,
                                               QuayCode = bo.QuayCode,
                                               BerthCode = bo.BerthCode,
                                               BollardCode = bo.BollardCode,
                                               BollardName = bo.BollardName,
                                               FromMeter = bo.FromMeter,
                                               ToMeter = bo.ToMeter,
                                               Continous = bo.Continuous,
                                               ContinousStatus = bo.Continuous == "Y" ? false : true
                                           }).OrderBy(bo => bo.FromMeter).ToList()

                                       }).OrderBy(q => q.Min)
                                  select new BerthsData
                                  {
                                      PortCode = q.PortCode,
                                      QuayCode = q.QuayCode,
                                      QuayLength = q.QuayLength,
                                      BerthCode = q.BerthCode,
                                      BerthName = q.BerthName,
                                      Lengthm = q.Lengthm,
                                      Draftm = q.Draftm,
                                      TidalDraft = q.TidalDraft,
                                      BerthType = q.BerthType,
                                      CargoType = q.CargoType,
                                      VesselTypes = q.VesselTypes,
                                      ReasonsForVisitType = q.ReasonsForVisitType,
                                      TerminalOperators = q.TerminalOperators,
                                      Bollards = q.Bollards.Select(bo => new BollardData { PortCode = bo.PortCode, QuayCode = bo.QuayCode, BerthCode = bo.BerthCode, BollardName = bo.BollardName, BollardCode = bo.BollardCode, FromMeter = bo.FromMeter, ToMeter = bo.ToMeter, Continous = bo.Continous, ContinousStatus = bo.ContinousStatus }).ToList()
                                  }).ToList();

            return berthsWithBollards;
        }

        /// <summary>
        /// To Get BerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="quaycode"></param>
        /// <returns></returns>
        public List<QuayBerthBollardData> GetQuayBerthsBollard(string portCode)
        {
            List<BerthsData> berthswithbollards = new List<BerthsData>();
            List<QuayBerthBollardData> quaysberthbollards = new List<QuayBerthBollardData>();
            List<QuayVO> quays = (from a in _unitOfWork.Repository<Quay>().Queryable()
                                  where (a.PortCode == portCode
                                  && a.RecordStatus == RecordStatus.Active
                                  )
                                  select new QuayVO
                                  {
                                      PortCode = a.PortCode,
                                      QuayCode = a.QuayCode,
                                      QuayName = a.QuayName,
                                      QuayLength = a.QuayLength
                                  }).ToList();

            berthswithbollards = GetQuayBerthsBollard_BerthsWithBollards(portCode, berthswithbollards);

            foreach (BerthsData berth in berthswithbollards)
            {
                List<string> CargoTypesList = berth.CargoType;
                string CargoTypesNameList = "";
                foreach (string cargotype in CargoTypesList)
                {
                    string CargoTypeName = _unitOfWork.Repository<SubCategory>().Queryable().Where(s => s.SubCatCode == cargotype).Select(s => s.SubCatName).FirstOrDefault();
                    if (string.IsNullOrEmpty(CargoTypesNameList))
                        CargoTypesNameList = CargoTypeName;
                    else
                        CargoTypesNameList = CargoTypesNameList + ',' + CargoTypeName;
                }

                berth.CargoTypeNames = CargoTypesNameList;
            }

            foreach (QuayVO quay in quays)
            {
                QuayBerthBollardData qbb = new QuayBerthBollardData();
                qbb.QuayCode = quay.QuayCode;
                qbb.QuayLength = quay.QuayLength;
                qbb.QuayName = quay.QuayName;
                qbb.Berths = berthswithbollards.Where(p => p.QuayCode == quay.QuayCode).ToList();
                quaysberthbollards.Add(qbb);
            }

            return quaysberthbollards;
        }

        private List<BerthsData> GetQuayBerthsBollard_BerthsWithBollards(string portCode, List<BerthsData> berthsWithBollards)
        {
            berthsWithBollards = (from q in
                                      (from b in _unitOfWork.Repository<Berth>().Queryable()
                                                   .Include(b => b.Quay)
                                                   .Include(b => b.BerthCargoes)
                                                   .Include(b => b.BerthVesselTypes)
                                                   .Include(b => b.BerthReasonForVisits)
                                                   .Include(b => b.TerminalOperatorBerths)
                                                   .Include(b => b.Bollards)
                                                   .Where(b => b.PortCode == portCode
                                                     && b.Bollards.Count > 0
                                                      && b.RecordStatus == RecordStatus.Active && b.BerthType != DryDockStatus.DryDockType
                                                      )
                                       select new
                                       {
                                           PortCode = b.PortCode,
                                           QuayCode = b.QuayCode,
                                           QuayLength = b.Quay.QuayLength,
                                           BerthCode = b.BerthCode,
                                           BerthName = b.BerthName,
                                           TidalDraft = b.TidalDraft,
                                           Min = b.Bollards.Min(bo => bo.FromMeter),
                                           Lengthm = b.Lengthm,
                                           Draftm = b.Draftm,
                                           BerthType = b.BerthType,
                                           CargoType = (b.BerthCargoes.Select(bc => bc.CargoTypeCode)).ToList<String>(),
                                           VesselTypes = (b.BerthVesselTypes.Select(bvt => bvt.VesselTypeCode)).ToList<String>(),
                                           ReasonsForVisitType = (b.BerthReasonForVisits.Select(rfv => rfv.ReasonForVisitCode)).ToList<String>(),
                                           TerminalOperators = (b.TerminalOperatorBerths.Select(to => to.TerminalOperatorID)).ToList<int>(),
                                           Bollards = b.Bollards.Select(bo => new
                                           {
                                               PortCode = bo.PortCode,
                                               QuayCode = bo.QuayCode,
                                               BerthCode = bo.BerthCode,
                                               BollardCode = bo.BollardCode,
                                               BollardName = bo.BollardName,
                                               FromMeter = bo.FromMeter,
                                               ToMeter = bo.ToMeter,
                                               Continous = bo.Continuous,
                                               Coordinates = bo.Coordinates,
                                               ContinousStatus = bo.Continuous == "Y" ? false : true
                                           }).OrderBy(bo => bo.FromMeter).ToList()

                                       }).OrderBy(q => q.Min)
                                  select new BerthsData
                                  {
                                      PortCode = q.PortCode,
                                      QuayCode = q.QuayCode,
                                      QuayLength = q.QuayLength,
                                      BerthCode = q.BerthCode,
                                      BerthName = q.BerthName,
                                      Lengthm = q.Lengthm,
                                      Draftm = q.Draftm,
                                      TidalDraft = q.TidalDraft,
                                      BerthType = q.BerthType,
                                      CargoType = q.CargoType,
                                      VesselTypes = q.VesselTypes,
                                      ReasonsForVisitType = q.ReasonsForVisitType,
                                      TerminalOperators = q.TerminalOperators,
                                      Bollards = q.Bollards.Select(bo => new BollardData { PortCode = bo.PortCode, QuayCode = bo.QuayCode, BerthCode = bo.BerthCode, BollardName = bo.BollardName, BollardCode = bo.BollardCode, FromMeter = bo.FromMeter, ToMeter = bo.ToMeter, Continous = bo.Continous, Coordinates = bo.Coordinates, ContinousStatus = bo.ContinousStatus }).ToList()
                                  }).ToList();

            return berthsWithBollards;
        }

        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portcode"></param>
        ///<param name="userid"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        public List<BerthPlanningVO> GetVesselInformation(string portCode, int userId, string userType, string fromDate, string toDate)
        {
            DateTime fromdate = DateTime.Parse(fromDate, CultureInfo.InvariantCulture);
            List<BerthPlanningVO> VesselCallMovements;

            if (userType == "TO")
            {
                int UserTypeID = (from u in _unitOfWork.Repository<User>().Queryable()
                                  where u.UserID == userId
                                  select u.UserTypeID).FirstOrDefault<int>();

                var _fromDate = new SqlParameter("@FromDate", fromdate);
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", UserTypeID);

                VesselCallMovements = _unitOfWork.SqlQuery<BerthPlanningVO>("usp_GetVesselInformation @FromDate,@PortCode,@UserType,@UserID", _fromDate, _portCode, _userType, _userID).ToList();
            }
            else
            {
                var _fromDate = new SqlParameter("@FromDate", fromDate);
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", userId);

                VesselCallMovements = _unitOfWork.SqlQuery<BerthPlanningVO>("usp_GetVesselInformation @FromDate,@PortCode,@UserType,@UserID", _fromDate, _portCode, _userType, _userID).ToList();

            }

            VesselCallMovements = VesselCallMovements.Where(vcm => (vcm.MovementStatus == "MPEN" && DateTime.Parse(fromDate, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) || (((DateTime.Parse(fromDate, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromDate, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toDate, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toDate, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))))).ToList();

            var plannedvessels = VesselCallMovements.FindAll(m => m.MovementStatus == MovementStatus.PENDING).ToList();
            var unplannedVessels = VesselCallMovements.FindAll(m => m.MovementStatus != MovementStatus.PENDING).OrderBy(e => e.ETB).ToList();

            unplannedVessels = unplannedVessels.Union(plannedvessels).ToList();
            return unplannedVessels;
        }

        public List<BerthMaintenanceData> GetBerthMaintenance(string portCode, int userId, string fromDate, string toDate)
        {
            DateTime _fromDate = DateTime.Parse(fromDate, CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.Parse(toDate, CultureInfo.InvariantCulture);
            List<BerthMaintenanceData> BerthMainList = (from b in _unitOfWork.Repository<BerthMaintenance>().Query()
                                                        .Include(b => b.WorkflowInstance)
                                                            .Select()
                                                        where b.MaintPortCode == portCode && b.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved &&
                                                        ((b.PeriodFrom >= _fromDate && b.PeriodFrom <= _toDate) || (b.PeriodTo >= _fromDate && b.PeriodTo <= _toDate))

                                                        select new BerthMaintenanceData
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
                                                            WorkflowInstanceId = b.WorkflowInstanceId,
                                                            Length = b.MaintBerthCode != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == b.ToPortCode && bo.QuayCode == b.ToQuayCode && bo.BerthCode == b.ToBerthCode && bo.BollardCode == b.ToBollard).Select(bo => bo.ToMeter).FirstOrDefault() : 0,
                                                            PositionX = b.FromBollard != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == b.FromPortCode && bo.QuayCode == b.FromQuayCode && bo.BollardCode == b.FromBollard && bo.BerthCode == b.FromBerthCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
                                                            PositionY = b.PeriodFrom.Date == _fromDate.Date ? b.PeriodFrom.Hour : b.PeriodFrom.Hour + 24,
                                                        }).ToList();

            return BerthMainList;
        }

        public List<BerthPlanningConfiguration> GetBerthPlanningConfigurations(string portCode)
        {
            List<BerthPlanningConfiguration> bpcdata = new List<BerthPlanningConfiguration>();
            bpcdata = (from pgc in _unitOfWork.Repository<PortGeneralConfig>().Queryable()
                       where pgc.PortCode == portCode && pgc.GroupName == "Berth Planning"
                       select new BerthPlanningConfiguration
                       {
                           ConfigName = pgc.ConfigName,
                           ConfigValue = pgc.ConfigValue,
                           ConfigLabelName = pgc.ConfigValue
                       }).ToList();

            return bpcdata;
        }

        public Boolean CheckBerthAvailability(string portCode, string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime)
        {
            Decimal FromMeter = 0;
            Decimal ToMeter = 0;

            if (!string.IsNullOrEmpty(fromBollardMeter))
            {
                FromMeter = Convert.ToDecimal((fromBollardMeter.Trim()).Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(toBollardMeter))
            {
                ToMeter = Convert.ToDecimal((toBollardMeter.Trim()).Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            List<AvailabilityVO> VesselCallMovements = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
                                                        where vcm.FromPositionPortCode == portCode && vcm.FromPositionQuayCode == quayCode && vcm.MovementStatus != "MPEN" && vcm.VesselCallMovementID != Convert.ToInt32(vcmId, CultureInfo.InvariantCulture) && vcm.RecordStatus == RecordStatus.Active
                                                        select new AvailabilityVO
                                                        {
                                                            VCN = vcm.VCN,
                                                            FromPortCode = vcm.FromPositionPortCode,
                                                            ToPortCode = vcm.ToPositionPortCode,
                                                            FromQuayCode = vcm.FromPositionQuayCode,
                                                            ToQuayCode = vcm.ToPositionQuayCode,
                                                            FromBerthCode = vcm.FromPositionBerthCode,
                                                            ToBerthCode = vcm.ToPositionBerthCode,
                                                            FromBollardMeter = vcm.FromPositionBollardCode != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == vcm.FromPositionPortCode && bo.QuayCode == vcm.FromPositionQuayCode && bo.BerthCode == vcm.FromPositionBerthCode && bo.BollardCode == vcm.FromPositionBollardCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
                                                            ToBollardMeter = vcm.ToPositionBollardCode != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == vcm.ToPositionPortCode && bo.QuayCode == vcm.ToPositionQuayCode && bo.BerthCode == vcm.ToPositionBerthCode && bo.BollardCode == vcm.ToPositionBollardCode).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
                                                            FromBollardCode = vcm.FromPositionBollardCode,
                                                            ToBollardCode = vcm.ToPositionBollardCode,
                                                            BerthTime = (vcm.ATB == null ? vcm.ETB : vcm.ATB) ?? DateTime.MinValue,
                                                            UnBerthTime = (vcm.ATUB == null ? vcm.ETUB : vcm.ATUB) ?? DateTime.MinValue,
                                                        }).ToList();

            VesselCallMovements = VesselCallMovements.Where(vcm => (DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(vcm.BerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(vcm.UnBerthTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();
            VesselCallMovements = VesselCallMovements.Where(vcm => (vcm.FromBollardMeter >= FromMeter && vcm.FromBollardMeter <= ToMeter) || ((FromMeter >= vcm.FromBollardMeter && (FromMeter <= vcm.ToBollardMeter)) || (ToMeter >= vcm.FromBollardMeter) && (ToMeter <= vcm.ToBollardMeter))).ToList();

            List<BerthMaintenanceData> BerthMainList = (from b in _unitOfWork.Repository<BerthMaintenance>().Query()
                                                        .Include(b => b.WorkflowInstance)
                                                            .Select()
                                                        where b.MaintPortCode == portCode && b.MaintQuayCode == quayCode && b.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved && b.RecordStatus == RecordStatus.Active
                                                        select new BerthMaintenanceData
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
                                                            FromBollardMeter = b.FromBollard != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == b.FromPortCode && bo.QuayCode == b.FromQuayCode && bo.BerthCode == b.FromBerthCode && bo.BollardCode == b.FromBollard).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
                                                            ToBollardMeter = b.ToBollard != null ? _unitOfWork.Repository<Bollard>().Query().Select().Where(bo => bo.PortCode == b.ToPortCode && bo.QuayCode == b.ToQuayCode && bo.BerthCode == b.ToBerthCode && bo.BollardCode == b.ToBollard).Select(bo => bo.FromMeter).FirstOrDefault() : 0,
                                                            WorkflowInstanceId = b.WorkflowInstanceId
                                                        }).ToList();

            BerthMainList = BerthMainList.Where(bm => (DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(fromTime, CultureInfo.InvariantCulture) && DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(toTime, CultureInfo.InvariantCulture)) || ((DateTime.Parse(fromTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(fromTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture))) || ((DateTime.Parse(toTime, CultureInfo.InvariantCulture) >= DateTime.Parse(bm.PeriodFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)) && (DateTime.Parse(toTime, CultureInfo.InvariantCulture) <= DateTime.Parse(bm.PeriodTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)))).ToList();
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

            if (count == 0 && IsBerthMaintainence == false)
                return true;
            else
                return false;
        }

        public UserData GetUserDetails(string portCode, int userId)
        {
            UserData userdata = new UserData();
            bool IsTerminalOperator = false;
            bool IsBerthPlanner = false;
            var users = _unitOfWork.Repository<User>().Queryable()
                        .Join(_unitOfWork.Repository<UserRole>().Queryable(), u => u.UserID, ur => ur.UserID, (u, ur) => new { u, ur })
                        .Join(_unitOfWork.Repository<Role>().Queryable(), rur => rur.ur.RoleID, ruo => ruo.RoleID, (rur, ruo) => new { rur, ruo })
                        .Where(x => x.rur.u.UserID == userId)
                        .Select(x => new
                        {
                            UserID = x.rur.u.UserID,
                            RoleID = x.rur.ur.RoleID,
                            RoleCode = x.ruo.RoleCode,
                            RoleName = x.ruo.RoleName,
                            UserTypeID = x.rur.u.UserTypeID,
                            UserType = x.rur.u.UserType
                        }).ToList();


            IsTerminalOperator = users.Exists(x => x.UserType == "TO");
            if (!IsTerminalOperator)
                IsBerthPlanner = users.Exists(x => x.RoleCode == "BP");

            if (IsTerminalOperator)
            {
                var TODATA = (from t in users where t.UserType == "TO" select t).FirstOrDefault();
                userdata.UserID = TODATA.UserID;
                userdata.RoleID = TODATA.RoleID;
                userdata.RoleCode = TODATA.RoleCode;
                userdata.RoleName = TODATA.RoleName;
                userdata.UserTypeID = TODATA.UserTypeID;
                userdata.isTerminalOperator = true;
                userdata.isBerthPlanner = false;
                userdata.UserType = TODATA.UserType;
                List<String> toberths = _unitOfWork.Repository<TerminalOperatorBerth>().Queryable().Where(x => x.TerminalOperatorID == 1).Select(x => x.BerthCode).ToList<String>();
                userdata.Berths = toberths;
            }
            else if (IsBerthPlanner)
            {
                var BPDATA = (from t in users where t.RoleCode == "BP" select t).FirstOrDefault();
                userdata.UserID = BPDATA.UserID;
                userdata.RoleID = BPDATA.RoleID;
                userdata.RoleCode = BPDATA.RoleCode;
                userdata.RoleName = BPDATA.RoleName;
                userdata.UserTypeID = BPDATA.UserTypeID;
                userdata.UserType = BPDATA.UserType;
                userdata.isBerthPlanner = true;
                userdata.isTerminalOperator = false;
            }
            else
            {
                var Data = (from t in users select t).FirstOrDefault();
                userdata.UserID = Data.UserID;
                userdata.RoleID = Data.RoleID;
                userdata.RoleCode = Data.RoleCode;
                userdata.RoleName = Data.RoleName;
                userdata.UserTypeID = Data.UserTypeID;
                userdata.UserType = Data.UserType;
                userdata.isTerminalOperator = false;
                userdata.isBerthPlanner = false;
            }

            return userdata;
        }

        /// <summary>
        /// To Update VesselCallMovementInformation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedVessels"></param>
        /// <returns></returns>
        public List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels, int userId)
        {
            if (plannedVessels != null)
            {
                foreach (BerthPlanningVO plannedvessel in plannedVessels)
                {
                    string VCN = plannedvessel.VCN;
                    string Slot;
                    string SlotStatus;
                    string DoubleBankedVessel;
                    var movementStatus = string.Empty;

                    if (String.IsNullOrEmpty(plannedvessel.DoubleBankedVessel))
                        DoubleBankedVessel = null;
                    else
                        DoubleBankedVessel = plannedvessel.DoubleBankedVessel;

                    int IsBanked = plannedvessel.IsBanked;
                    int VCMID = plannedvessel.VesselCallMovementID;
                    VesselCallMovement vesselcallmovement = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select() where vcm.VCN == VCN && vcm.VesselCallMovementID == VCMID select vcm).SingleOrDefault();
                    if (vesselcallmovement != null)
                    {
                        movementStatus = vesselcallmovement.MovementStatus;
                        vesselcallmovement.ETB = plannedvessel.ETB;
                        vesselcallmovement.ETUB = plannedvessel.ETUB;
                        vesselcallmovement.FromPositionPortCode = plannedvessel.FromPortCode;
                        vesselcallmovement.FromPositionQuayCode = plannedvessel.FromQuayCode;
                        vesselcallmovement.FromPositionBerthCode = plannedvessel.FromBerthCode;
                        vesselcallmovement.FromPositionBollardCode = plannedvessel.FromBollardCode;
                        vesselcallmovement.ToPositionPortCode = plannedvessel.ToPortCode;
                        vesselcallmovement.ToPositionQuayCode = plannedvessel.ToQuayCode;
                        vesselcallmovement.ToPositionBerthCode = plannedvessel.ToBerthCode;
                        vesselcallmovement.ToPositionBollardCode = plannedvessel.ToBollardCode;
                        vesselcallmovement.MovementStatus = plannedvessel.MovementStatus;
                        vesselcallmovement.ModifiedBy = userId;
                        vesselcallmovement.ModifiedDate = System.DateTime.Now;
                        if (plannedvessel.MovementStatus != "BERT" && plannedvessel.MovementStatus != "SALD")
                        {
                            if (plannedvessel.MovementStatus == "CONF")
                            {
                                if (plannedvessel.MovementType == MovementTypes.ARRIVAL || plannedvessel.MovementType == MovementTypes.SHIFTING)
                                {
                                    //SlotStatus = "PLND";                                

                                    if (movementStatus == MovementStatus.CONFIRMED || movementStatus == MovementStatus.BERTHED || movementStatus == MovementStatus.SAILED)
                                    {
                                        SlotStatus = vesselcallmovement.SlotStatus;
                                        Slot = vesselcallmovement.Slot;
                                    }
                                    else
                                    {
                                        vesselcallmovement.SlotDate = plannedvessel.ETB;

                                        if (vesselcallmovement.MovementType == MovementTypes.SHIFTING)
                                        {
                                            SlotStatus = null;
                                            Slot = GetSlotPeriodBySlotdate(plannedvessel.ETB ?? DateTime.MinValue, plannedvessel.FromPortCode);
                                        }
                                        else
                                        {
                                            SlotStatus = "PLND";
                                            Slot = vesselcallmovement.Slot;
                                        }
                                    }

                                    //vesselcallmovement.MovementDateTime = plannedvessel.ETB;
                                    //vesselcallmovement.SlotDate = plannedvessel.ETB;

                                    if (vesselcallmovement.MovementType == MovementTypes.SHIFTING)
                                    {
                                        //SlotStatus = null;
                                        //Slot = GetSlotPeriodBySlotdate(plannedvessel.ETB ?? DateTime.MinValue, plannedvessel.FromPortCode);
                                        _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1,FromPositionQuayCode = @p2, FromPositionBerthCode = @p3, FromPositionBollardCode = @p4, ToPositionPortCode = @p5, ToPositionQuayCode = @p6, ToPositionBerthCode = @p7, ToPositionBollardCode = @p8, ModifiedBy = @p9, ModifiedDate =  @p10, MovementStatus = @p11, IsBanked=@p12, DoubleBankedVessel=@p13 ,Slot=@p14, SlotStatus=@p15, MovementDateTime=@p16, SlotDate=@p17 where VesselCallMovementID = @p18", plannedvessel.ETB, plannedvessel.ETUB, plannedvessel.FromQuayCode, plannedvessel.FromBerthCode, plannedvessel.FromBollardCode, plannedvessel.ToPortCode, plannedvessel.ToQuayCode, plannedvessel.ToBerthCode, plannedvessel.ToBollardCode, userId, System.DateTime.Now, plannedvessel.MovementStatus, IsBanked, DoubleBankedVessel, Slot, SlotStatus, plannedvessel.ETB, vesselcallmovement.SlotDate, VCMID);

                                        ShiftingServiceRequest(plannedvessel.VCN);
                                    }
                                    else
                                    {
                                        _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1,FromPositionQuayCode = @p2, FromPositionBerthCode = @p3, FromPositionBollardCode = @p4, ToPositionPortCode = @p5, ToPositionQuayCode = @p6, ToPositionBerthCode = @p7, ToPositionBollardCode = @p8, ModifiedBy = @p9, ModifiedDate =  @p10, MovementStatus = @p11, IsBanked=@p12, DoubleBankedVessel=@p13, SlotStatus=@p14, MovementDateTime=@p15 where VesselCallMovementID = @p16", plannedvessel.ETB, plannedvessel.ETUB, plannedvessel.FromQuayCode, plannedvessel.FromBerthCode, plannedvessel.FromBollardCode, plannedvessel.ToPortCode, plannedvessel.ToQuayCode, plannedvessel.ToBerthCode, plannedvessel.ToBollardCode, userId, System.DateTime.Now, plannedvessel.MovementStatus, IsBanked, DoubleBankedVessel, SlotStatus, plannedvessel.ETB, VCMID);
                                    }
                                }
                            }
                            else if (movementStatus == "BERT" || movementStatus == "SALD")
                            {

                            }
                            else
                            {
                                _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set ETB = @p0, ETUB = @p1,FromPositionQuayCode = @p2, FromPositionBerthCode = @p3, FromPositionBollardCode = @p4,ToPositionPortCode = @p5,ToPositionQuayCode = @p6, ToPositionBerthCode = @p7, ToPositionBollardCode = @p8, ModifiedBy = @p9, ModifiedDate =  @p10, MovementStatus = @p11 , IsBanked=@p12,DoubleBankedVessel=@p13 where VesselCallMovementID = @p14", plannedvessel.ETB, plannedvessel.ETUB, plannedvessel.FromQuayCode, plannedvessel.FromBerthCode, plannedvessel.FromBollardCode, plannedvessel.ToPortCode, plannedvessel.ToQuayCode, plannedvessel.ToBerthCode, plannedvessel.ToBollardCode, userId, System.DateTime.Now, plannedvessel.MovementStatus, IsBanked, DoubleBankedVessel, VCMID);
                            }
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(plannedvessel.DoubleBankedVessel))
                            {
                                _unitOfWork.ExecuteSqlCommand("Update VesselCallMovement set IsBanked=@p0,DoubleBankedVessel=@p1 where VesselCallMovementID = @p2", IsBanked, DoubleBankedVessel, VCMID);
                            }
                        }
                    }
                }
            }

            return plannedVessels;
        }

        private string GetSlotPeriodBySlotdate(DateTime SlotDateTime, string port)
        {
            int hours = SlotDateTime.Hour;
            string slotPeriod = string.Empty;
            double totalminutes = SlotDateTime.TimeOfDay.TotalMinutes;

            if (SlotDateTime != DateTime.MinValue)
            {
                foreach (ResourceSlotVO slot in _suppServiceResourceAllocRepository.GetSlotConfiguration(SlotDateTime, port))
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

        public void ShiftingServiceRequest(string vcn)
        {
            var vcm = (from vm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(vm => vm.VCN == vcn && vm.RecordStatus == RecordStatus.Active)
                       .Include(vm => vm.ServiceRequest)
                       //.Select()
                       //    where vm.VCN == vcn && vm.RecordStatus == RecordStatus.Active
                       select vm).ToList();

            var arr = vcm.Find(a => a.MovementType == MovementTypes.ARRIVAL);

            if (arr != null)
            {
                if (arr.MovementStatus == MovementStatus.BERTHED)
                {
                    var shf =
                        vcm.FindAll(
                            a => a.MovementType == MovementTypes.SHIFTING && a.RecordStatus == RecordStatus.Active && a.ServiceRequest.RecordStatus == RecordStatus.Active)
                            .ToList();

                    if (shf.Count > 0)
                    {
                        if (shf.Count == 1)
                        {
                            var shift = shf.FirstOrDefault();
                            if (shift == null) return;
                            if (shift.ServiceRequest.WorkflowInstanceId == null)
                            {
                                AddWorkflowInstanceAndWorkflowProcess(Convert.ToInt32(shift.ServiceRequestID, CultureInfo.InvariantCulture));
                            }
                        }
                        else
                        {
                            var activeShifts = shf.FindAll(a => a.ServiceRequest.WorkflowInstanceId != null);
                            var berthedShifts = shf.FindAll(a => a.MovementStatus == MovementStatus.BERTHED);

                            var count = 0;

                            if (activeShifts.Count > 0)
                            {
                                if (activeShifts.Count == berthedShifts.Count)
                                {
                                    count +=
                                        berthedShifts.Select(
                                            shift =>
                                                _unitOfWork.SqlQuery<string>(
                                                    "select TaskStatus from ResourceAllocation where OperationType = @p0 and ServiceReferenceID = @p1 and ServiceReferenceType = @p2 and RecordStatus = 'A'",
                                                    ServiceTypeCode.BerthMaster, shift.ServiceRequestID,
                                                    ServiceReferenceType.VeselTraficServices)
                                                    .ToList()
                                                    .Any(a => a.Contains(ResourceAllcationWorkFlowStatus.Verified) || a.Contains(ResourceAllcationWorkFlowStatus.Completed)))
                                        //.Contains(ResourceAllcationWorkFlowStatus.Completed))
                                            .Count(verifyResAll => verifyResAll);
                                }
                            }

                            if (activeShifts.Count != count) return;
                            var shifting =
                                shf.FindAll(
                                    a =>
                                        a.ServiceRequest.WorkflowInstanceId == null &&
                                        a.MovementStatus == MovementStatus.SCHEDULED)
                                // a.MovementStatus == MovementStatus.CONFIRMED)
                                    .OrderBy(b => b.ETB)
                                    .FirstOrDefault();
                            if (shifting != null)
                            {
                                AddWorkflowInstanceAndWorkflowProcess(Convert.ToInt32(shifting.ServiceRequestID, CultureInfo.InvariantCulture));
                            }
                        }
                    }
                }
            }
        }

        private void AddWorkflowInstanceAndWorkflowProcess(int serviceRequestId)
        {
            var serviceObj = _unitOfWork.Repository<ServiceRequest>().Find(serviceRequestId);
            var entityId = _unitOfWork.SqlQuery<int>("select EntityID from Entity where EntityCode = @p0", EntityCodes.Service_Request).ToList().FirstOrDefault();
            var roleId = _unitOfWork.SqlQuery<int>("select RoleID from Role where RoleCode = @p0", Roles.Agent).ToList().FirstOrDefault();
            if (serviceObj == null) return;
            if (serviceObj.WorkflowInstanceId != null) return;
            var workflowInstanceObj = _unitOfWork.Repository<WorkflowInstance>().Find(serviceObj.BPWorkflowInstanceId);
            var workflowProcessObj = _unitOfWork.Repository<WorkflowProcess>().Find(workflowInstanceObj.WorkflowProcessId);

            WorkflowInstance workflowInstance = null;

            if (workflowInstanceObj != null)
            {
                workflowInstance = new WorkflowInstance();
                workflowInstance.EntityID = entityId;
                workflowInstance.PortCode = workflowInstanceObj.PortCode;
                workflowInstance.ReferenceID = workflowInstanceObj.ReferenceID;
                workflowInstance.WorkflowTaskCode = WFStatus.NewRequest;
                workflowInstance.UserTypeId = 0;
                workflowInstance.UserType = UserType.Employee;
                workflowInstance.RecordStatus = workflowInstanceObj.RecordStatus;
                workflowInstance.CreatedBy = workflowInstanceObj.CreatedBy;
                workflowInstance.CreatedDate = workflowInstanceObj.CreatedDate;
                workflowInstance.ModifiedBy = workflowInstanceObj.ModifiedBy;
                workflowInstance.ModifiedDate = workflowInstanceObj.ModifiedDate;
                workflowInstance.ObjectState = ObjectState.Added;

                _unitOfWork.Repository<WorkflowInstance>().Insert(workflowInstance);
                _unitOfWork.SaveChanges();
            }

            WorkflowProcess workflowProcess = null;

            if (workflowProcessObj != null)
            {
                workflowProcess = new WorkflowProcess();
                workflowProcess.RoleId = roleId;
                workflowProcess.FromTaskCode = WFStatus.NewRequest;
                workflowProcess.ToTaskCode = WFStatus.NewRequest;
                workflowProcess.ReferenceData = workflowProcessObj.ReferenceData;
                workflowProcess.Remarks = "New Service Request";
                workflowProcess.RecordStatus = workflowProcessObj.RecordStatus;
                workflowProcess.CreatedBy = workflowProcessObj.CreatedBy;
                workflowProcess.CreatedDate = workflowProcessObj.CreatedDate;
                workflowProcess.ModifiedBy = workflowProcessObj.ModifiedBy;
                workflowProcess.ModifiedDate = workflowProcessObj.ModifiedDate;
                workflowProcess.WorkflowInstanceId = workflowInstance.WorkflowInstanceId;
                workflowProcess.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<WorkflowProcess>().Insert(workflowProcess);
                _unitOfWork.SaveChanges();
            }

            _unitOfWork.ExecuteSqlCommand("Update ServiceRequest set WorkflowInstanceId = @p0 where ServiceRequestID = @p1", workflowInstance.WorkflowInstanceId, serviceRequestId);
        }

        public List<BerthPlanningVO> GetVesselInformationGIS(string portCode, int userId, string userType, string fromDate, string toDate)
        {
            DateTime fromdate = DateTime.Parse(fromDate, CultureInfo.InvariantCulture);
            List<BerthPlanningVO> VesselCallMovements;

            if (userType == "TO")
            {
                int UserTypeID = (from u in _unitOfWork.Repository<User>().Query().Select()
                                  where u.UserID == userId
                                  select u.UserTypeID).FirstOrDefault<int>();

                var _fromDate = new SqlParameter("@FromDate", fromdate);
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", UserTypeID);

                VesselCallMovements = _unitOfWork.SqlQuery<BerthPlanningVO>("usp_GetVesselInformationGIS @FromDate,@PortCode,@UserType,@UserID", _fromDate, _portCode, _userType, _userID).ToList();
            }
            else
            {
                var _fromDate = new SqlParameter("@FromDate", fromDate);
                var _portCode = new SqlParameter("@PortCode", portCode);
                var _userType = new SqlParameter("@UserType", userType);
                var _userID = new SqlParameter("@UserID", userId);

                VesselCallMovements = _unitOfWork.SqlQuery<BerthPlanningVO>("usp_GetVesselInformationGIS @FromDate,@PortCode,@UserType,@UserID", _fromDate, _portCode, _userType, _userID).ToList();
            }


            foreach (BerthPlanningVO vcm in VesselCallMovements)
            {
                List<PreviousBollardData> _previousbollarddata = (from b in _unitOfWork.Repository<Bollard>().Queryable()
                                                                  orderby b.FromMeter ascending
                                                                  where (b.FromMeter >= vcm.FromBollardMeter && b.FromMeter <= vcm.ToBollardMeter && b.PortCode == vcm.FromPortCode && b.QuayCode == vcm.FromQuayCode)
                                                                  select new PreviousBollardData
                                                                  {
                                                                      PortCode = b.PortCode,
                                                                      QuayCode = b.QuayCode,
                                                                      BerthCode = b.BerthCode,
                                                                      BollardCode = b.BollardCode,
                                                                      FromMeter = b.FromMeter,
                                                                      ToMeter = b.ToMeter,
                                                                      Coordinates = b.Coordinates,
                                                                      OffsetCoordinates = b.OffsetCoordinates
                                                                  }).ToList();
                if (_previousbollarddata.Count > 2)
                {
                    PreviousBollardData PreviousBollardData_obj = _previousbollarddata[_previousbollarddata.Count - 2];

                    string precoord = PreviousBollardData_obj.Coordinates;
                    string preoffsetcord = PreviousBollardData_obj.OffsetCoordinates;

                    vcm.precoord = PreviousBollardData_obj.Coordinates;
                    vcm.preoffsetcord = PreviousBollardData_obj.OffsetCoordinates;
                }
            }

            return VesselCallMovements;
        }
        //anchored code
        public List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portCode)
        {
            var PortCode = new SqlParameter("@PortCode", portCode);
            var AnchoredvesselList = _unitOfWork.SqlQuery<AnchorVesselInfoGISVO>("dbo.usp_GetAnchorVesselInfoGIS @PortCode", PortCode).ToList();

            return AnchoredvesselList;
        }

        public GISMapPathVo GetGisMapPath(string portCode)
        {
            GISMapPathVo obj = new GISMapPathVo();
            var berthPlanningConfiguration = GetBerthPlanningConfigurations(portCode);

            obj.geographicLocation = (from pgc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()
                                      where pgc.PortCode == portCode && pgc.GroupName == "Berth Planning" && pgc.ConfigName == "GeographicLocation"
                                      select pgc).FirstOrDefault().ConfigValue;
            obj.mapPath = ConfigurationSettings.AppSettings["GISMapPath"] + portCode;

            return obj;
        }

        public List<BerthedVessels> GetBerthedVesselDetails(string portcode)
        {
            var Portcode = new SqlParameter("@PortCode", portcode);
            var BerthedList = _unitOfWork.SqlQuery<BerthedVessels>("dbo.usp_GetBerthedVesselDtls @PortCode", Portcode).ToList();

            return BerthedList;
        }
        public List<SailedVessels> GetSailedVesselDetails(string portcode)
        {
            var Portcode = new SqlParameter("@PortCode", portcode);
            var SailedList = _unitOfWork.SqlQuery<SailedVessels>("dbo.usp_GetSailedVesselDtls @PortCode", Portcode).ToList();

            return SailedList;
        }
        public List<AnchoredVessels> GetAnchoredVesselDetails(string portcode)
        {
            var Portcode = new SqlParameter("@PortCode", portcode);
            var AnchoredList = _unitOfWork.SqlQuery<AnchoredVessels>("dbo.usp_GetanchoredVesselDtls @PortCode", Portcode).ToList();

            return AnchoredList;
        }
    }
}

