using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data.Entity;
using IPMS.Domain;
using System.Data.SqlClient;
namespace IPMS.Repository
{
    public class BerthRepository : IBerthRepository
    {
        private IUnitOfWork _unitOfWork;

        public BerthRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }

        #region GetDryDocBerths
        public List<BerthVO> GetDryDocBerths(string portCode)
        {
            var Berth = (from berthvo in _unitOfWork.Repository<Berth>().Queryable()
                         where berthvo.PortCode == portCode && berthvo.BerthType == "DRDK"
                         select new BerthVO
                         {
                             BerthKey = berthvo.PortCode + "." + berthvo.QuayCode + "." + berthvo.BerthCode,
                             PortCode = berthvo.PortCode,
                             QuayCode = berthvo.QuayCode,
                             BerthCode = berthvo.BerthCode,
                             BerthName = berthvo.BerthName,
                             ShortName = berthvo.ShortName,
                             BerthType = berthvo.BerthType,
                             FromMeter = berthvo.FromMeter,
                             ToMeter = berthvo.ToMeter,
                             Lengthm = berthvo.Lengthm,
                             Draftm = berthvo.Draftm,
                             RecordStatus = berthvo.RecordStatus,
                             CreatedBy = berthvo.CreatedBy,
                             CreatedDate = berthvo.CreatedDate,
                             ModifiedBy = berthvo.ModifiedBy,
                             ModifiedDate = berthvo.ModifiedDate

                         });

            return Berth.ToList();
        }
        #endregion


        #region GetBerthsOnTerminalOperator
        public List<BerthVO> GetBerthsOnTerminalOperator(int toId)
        {
            var TerminalOperators = (from t in _unitOfWork.Repository<TerminalOperator>().Query()
                                           .Include(t => t.TerminalOperatorBerths)
                               .Include(t => t.TerminalOperatorBerths.Select(n => n.Berth))
                               .Include(t => t.TerminalOperatorBerths.Select(n => n.Berth.BerthCargoes))
                               .Select()
                                     where t.TerminalOperatorID == toId
                                     select t).ToList<TerminalOperator>();

            List<BerthVO> berthvoList = new List<BerthVO>();
            foreach (var to in TerminalOperators)
            {


                foreach (var brts in to.TerminalOperatorBerths)
                {
                    BerthVO berthvo = new BerthVO();
                    berthvo.BerthKey = brts.Berth.PortCode + "." + brts.Berth.QuayCode + "." + brts.Berth.BerthCode;
                    berthvo.BerthName = brts.Berth.BerthName;
                    berthvo.Draftm = brts.Berth.Draftm;

                    foreach (var berthCargo in brts.Berth.BerthCargoes)
                    {
                        if (string.IsNullOrWhiteSpace(berthvo.CargoDetails))
                        {
                            berthvo.CargoDetails = berthCargo.CargoTypeCode;
                        }
                        else
                        {
                            berthvo.CargoDetails = berthvo.CargoDetails + "," + berthCargo.CargoTypeCode;
                        }
                    }
                    berthvoList.Add(berthvo);
                }


            }

            return berthvoList;           

        }
        #endregion

        #region GetBerths
        public List<Berth> GetBerthsForArrival(string portCode)
        {

            var Berth = (from t in _unitOfWork.Repository<Berth>().Query()
                                .Include(t => t.BerthCargoes)
                                .Select()
                         where t.PortCode == portCode && t.RecordStatus == "A"
                         orderby t.BerthName
                         select t).ToList<Berth>();
            return Berth;      
        }
        #endregion



        #region GetBerths
        public List<BerthVO> GetBerths(string portCode)
        {
            var Berth = (from berthvo in _unitOfWork.Repository<Berth>().Queryable().Where(b => b.PortCode == portCode && b.RecordStatus == RecordStatus.Active)                         
                         orderby berthvo.BerthName
                         select new BerthVO
                         {
                             BerthKey = berthvo.PortCode + "." + berthvo.QuayCode + "." + berthvo.BerthCode,
                             PortCode = berthvo.PortCode,
                             QuayCode = berthvo.QuayCode,
                             BerthCode = berthvo.BerthCode,
                             BerthName = berthvo.BerthName,
                             ShortName = berthvo.ShortName,
                             BerthType = berthvo.BerthType,
                             FromMeter = berthvo.FromMeter,
                             ToMeter = berthvo.ToMeter,
                             Lengthm = berthvo.Lengthm,
                             Draftm = berthvo.Draftm,
                             RecordStatus = berthvo.RecordStatus,
                             CreatedBy = berthvo.CreatedBy,
                             CreatedDate = berthvo.CreatedDate,
                             ModifiedBy = berthvo.ModifiedBy,
                             ModifiedDate = berthvo.ModifiedDate

                         });

            return Berth.ToList();
        }
        #endregion

        #region GetBerthsDetails
        /// <summary>
        /// To Get Berth Details
        /// </summary>
        /// <returns></returns>
        public List<BerthVO> GetBerthsDetails(string portCode)
        {
            var berths = (from p in _unitOfWork.Repository<Port>().Queryable()
                          join qy in _unitOfWork.Repository<Quay>().Queryable()
                          on p.PortCode equals qy.PortCode
                          join b in _unitOfWork.Repository<Berth>().Queryable()
                          on new { a = qy.PortCode, b = qy.QuayCode } equals new { a = b.PortCode, b = b.QuayCode }
                          join s in _unitOfWork.Repository<SubCategory>().Queryable()
                          on b.BerthType equals s.SubCatCode
                          where b.PortCode == portCode
                          select new BerthVO
                          {
                              PortCode = b.PortCode,
                              PortName = p.PortName,
                              QuayCode = b.QuayCode,
                              QuayName = qy.QuayName,
                              QuayLength = qy.QuayLength,
                              BerthCode = b.BerthCode,
                              BerthName = b.BerthName,
                              BerthType = b.BerthType,
                              BerthTypeName = s.SubCatName,
                              ShortName = b.ShortName,
                              FromMeter = b.FromMeter,
                              ToMeter = b.ToMeter,
                              Lengthm = b.Lengthm,
                              Draftm = b.Draftm,
                              TidalDraft = b.TidalDraft,
                              RecordStatus = b.RecordStatus,
                              CreatedBy = b.CreatedBy,
                              CreatedDate = b.CreatedDate,
                              ModifiedBy = b.ModifiedBy,
                              ModifiedDate = b.ModifiedDate,
                          }).OrderByDescending(x=>x.CreatedDate).ToList<BerthVO>();
            GetValueBerthsDetails(berths);
            return berths;
        }

        private void GetValueBerthsDetails(List<BerthVO> berths)
        {
            foreach (var berth in berths)
            {
                var result = (from bc in _unitOfWork.Repository<BerthCargo>().Queryable()
                              join s in _unitOfWork.Repository<SubCategory>().Queryable() on bc.CargoTypeCode equals s.SubCatCode
                              where
                                  bc.BerthCode == berth.BerthCode && bc.PortCode == berth.PortCode && bc.QuayCode == berth.QuayCode &&
                                  bc.RecordStatus == "A"
                              select new { bc.CargoTypeCode }).Distinct().ToList();
                List<string> bcargotype = new List<string>();
                foreach (var x in result)
                {
                    bcargotype.Add(x.CargoTypeCode.ToString());
                }

                berth.CargoType = bcargotype;
            }


            foreach (var berth in berths)
            {
                var result = (from bv in _unitOfWork.Repository<BerthVesselType>().Queryable()
                              join s in _unitOfWork.Repository<SubCategory>().Queryable() on bv.VesselTypeCode equals s.SubCatCode
                              where
                                  bv.BerthCode == berth.BerthCode && bv.PortCode == berth.PortCode && bv.QuayCode == berth.QuayCode &&
                                  bv.RecordStatus == "A"
                              select new { bv.VesselTypeCode }).Distinct().ToList();
                List<string> bvesseltype = new List<string>();
                foreach (var x in result)
                {
                    bvesseltype.Add(x.VesselTypeCode.ToString());
                }

                berth.VesselType = bvesseltype;
            }


            foreach (var berth in berths)
            {
                var result = (from br in _unitOfWork.Repository<BerthReasonForVisit>().Queryable()
                              join s in _unitOfWork.Repository<SubCategory>().Queryable() on br.ReasonForVisitCode equals
                                  s.SubCatCode
                              where
                                  br.BerthCode == berth.BerthCode && br.PortCode == berth.PortCode && br.QuayCode == berth.QuayCode &&
                                  br.RecordStatus == "A"
                              select new { br.ReasonForVisitCode }).Distinct().ToList();
                List<string> breasontype = new List<string>();
                foreach (var x in result)
                {
                    breasontype.Add(x.ReasonForVisitCode.ToString());
                }

                berth.ReasonForVisitType = breasontype;
            }
        }

        #endregion

        #region ModifyBerth
        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public List<QuayVO> GetPortQuayDetails(string portId)
        {
            var portquays = (from a in _unitOfWork.Repository<Quay>().Queryable()
                             where a.PortCode == portId && a.RecordStatus == "A"
                             select new QuayVO
                             {
                                 QuayCode = a.QuayCode,
                                 QuayName = a.QuayName,
                                 QuayLength = a.QuayLength
                             }).OrderBy(x => x.QuayName).ToList<QuayVO>();
            return portquays;
        }
        #endregion

        #region GetBerthsInQuay
        /// <summary>
        /// To Get Berths In Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthVO> GetBerthsInQuay(string portCode, string quayCode)
        {
            var berths = (from p in _unitOfWork.Repository<Port>().Query().Select()
                          join qy in _unitOfWork.Repository<Quay>().Query().Select()
                          on p.PortCode equals qy.PortCode
                          join b in _unitOfWork.Repository<Berth>().Query().Select()
                          on new { a = qy.PortCode, b = qy.QuayCode } equals new { a = b.PortCode, b = b.QuayCode }
                          join s in _unitOfWork.Repository<SubCategory>().Query().Select()
                          on b.BerthType equals s.SubCatCode
                          orderby b.CreatedDate descending
                          where b.PortCode == portCode && b.QuayCode == quayCode
                          select new BerthVO
                          {
                              PortCode = b.PortCode,
                              PortName = p.PortName,
                              QuayCode = b.QuayCode,
                              QuayName = qy.QuayName,
                              QuayLength = qy.QuayLength,
                              BerthCode = b.BerthCode,
                              BerthName = b.BerthName,
                              BerthType = b.BerthType,
                              BerthTypeName = s.SubCatName,
                              ShortName = b.ShortName,
                              FromMeter = b.FromMeter,
                              ToMeter = b.ToMeter,
                              Lengthm = b.Lengthm,
                              Draftm = b.Draftm,
                              RecordStatus = b.RecordStatus,
                              CreatedBy = b.CreatedBy,
                              CreatedDate = b.CreatedDate,
                              ModifiedBy = b.ModifiedBy,
                              ModifiedDate = b.ModifiedDate,

                          }).ToList<BerthVO>();
            return berths;
        }
        #endregion

        #region GetBerthType
        /// <summary>
        /// To Get Berth Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetBerthType()
        {
            var BerthType = _unitOfWork.Repository<SubCategory>().Queryable().OrderBy(x => x.SubCatName).Where(x => x.SupCatCode == "BETY");
            return BerthType.ToList();
        }
        #endregion

        #region GetBerthsWithBollards
        /// <summary>
        /// //////////// By Mahesh ///////////////////////////////
        /// </summary>
        /// <returns></returns> // returns berthlist with correspond bollards.
        public List<BerthVO> GetBerthsWithBollards(string portCode)
        {
            var berths = (from b in _unitOfWork.Repository<Berth>().Queryable().Include(b => b.Bollards)
                          where b.PortCode == portCode
                          select b).OrderBy(x=>x.BerthName).ToList();
            return berths.MapToListDto();


        }
        #endregion


        #region AddBerth
        /// <summary>
        /// Add Berth
        /// </summary>
        /// <param name="berthData"></param>
        /// <param name="userId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public BerthVO AddBerth(BerthVO berthData, int userId, string portCode)
        {
            if (berthData != null)
            {
                if (!string.IsNullOrWhiteSpace(berthData.BerthName))
                {
                    berthData.BerthName = new string(CharsToTitleCase(berthData.BerthName).ToArray());
                }
                //  }

                berthData.CreatedBy = userId;
                berthData.CreatedDate = DateTime.Now;
                berthData.ModifiedBy = userId;
                berthData.ModifiedDate = DateTime.Now;
                berthData.PortCode = portCode;
                Berth berth = new Berth();
                berth = BerthMapExtension.MapToEntity(berthData);
                List<BerthCargo> berthCargoes = berth.BerthCargoes.ToList();
                berth.BerthCargoes = null;

                List<BerthVesselType> berthVessels = berth.BerthVesselTypes.ToList();
                berth.BerthVesselTypes = null;

                List<BerthReasonForVisit> berthReasons = berth.BerthReasonForVisits.ToList();
                berth.BerthReasonForVisits = null;

                berth.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Berth>().Insert(berth);
                _unitOfWork.SaveChanges();

                foreach (var berthcargo in berthCargoes)
                {
                    _unitOfWork.Repository<BerthCargo>().Insert(berthcargo);
                    _unitOfWork.SaveChanges();
                }

                foreach (var berthvessel in berthVessels)
                {
                    _unitOfWork.Repository<BerthVesselType>().Insert(berthvessel);
                    _unitOfWork.SaveChanges();
                }

                foreach (var berthreason in berthReasons)
                {
                    _unitOfWork.Repository<BerthReasonForVisit>().Insert(berthreason);
                    _unitOfWork.SaveChanges();
                }
            }
            return berthData;
        }
        #endregion

        #region ModifyBerth
        /// <summary>
        /// Update Berth 
        /// </summary>
        /// <param name="berthData"></param>
        /// <param name="userId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public BerthVO ModifyBerth(BerthVO berthData, int userId, string portCode)
        {
            if (berthData != null)
            {
                if (!string.IsNullOrWhiteSpace(berthData.BerthName))
                {
                    berthData.BerthName = new string(CharsToTitleCase(berthData.BerthName).ToArray());
                }
            }

            berthData.ModifiedBy = userId;
            berthData.ModifiedDate = DateTime.Now;
            berthData.PortCode = portCode;

            Berth berth = new Berth();
            berth = BerthMapExtension.MapToEntity(berthData);
            List<BerthCargo> berthCargoes = berth.BerthCargoes.ToList();
            berth.BerthCargoes = null;

            List<BerthVesselType> berthVessels = berth.BerthVesselTypes.ToList();
            berth.BerthVesselTypes = null;

            List<BerthReasonForVisit> berthReasons = berth.BerthReasonForVisits.ToList();
            berth.BerthReasonForVisits = null;

            GetValueModifyBerth(berth);

            ////// FOR BERTH CARGOES /////////

            GetValueCargoes(berthData, berthCargoes);

            ////// FOR BERTH VESSEL TYPES /////////

            GetValueVesselType(berthData, berthVessels);

            ////// FOR BERTH REASON FOR VISIT /////////

            var dbberthreasons = (from br in _unitOfWork.Repository<BerthReasonForVisit>().Query().Select().AsEnumerable()
                                  where br.PortCode == berthData.PortCode && br.QuayCode == berthData.QuayCode && br.BerthCode == berthData.BerthCode
                                  select br).ToList();

            if (dbberthreasons != null)
            {
                foreach (var br in berthReasons)
                {
                    var newreason = true;
                    foreach (var dbbr in dbberthreasons)
                    {
                        if (br.PortCode == dbbr.PortCode && br.QuayCode == dbbr.QuayCode && br.BerthCode == dbbr.BerthCode
                            && br.ReasonForVisitCode == dbbr.ReasonForVisitCode)
                        {
                            newreason = false;
                        }
                        if (br.PortCode == dbbr.PortCode && br.QuayCode == dbbr.QuayCode && br.BerthCode == dbbr.BerthCode
                            && br.ReasonForVisitCode == dbbr.ReasonForVisitCode && dbbr.RecordStatus == "I")
                        {
                            dbbr.RecordStatus = "A";
                            dbbr.ModifiedBy = dbbr.ModifiedBy;
                            dbbr.ModifiedDate = dbbr.ModifiedDate;
                            dbbr.BerthCode = dbbr.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                            _unitOfWork.Repository<BerthReasonForVisit>().Update(dbbr);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (newreason)
                    {
                        br.RecordStatus = "A";
                        br.ObjectState = ObjectState.Added;
                        br.BerthCode = br.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthReasonForVisit>().Insert(br);
                        _unitOfWork.SaveChanges();
                    }
                }

                foreach (var dbbr in dbberthreasons)
                {
                    var inactive = true;
                    foreach (var br in berthReasons)
                    {
                        if (dbbr.PortCode == br.PortCode && dbbr.QuayCode == br.QuayCode && dbbr.BerthCode == br.BerthCode && dbbr.ReasonForVisitCode == br.ReasonForVisitCode)
                        {
                            inactive = false;
                        }
                    }
                    if (inactive)
                    {
                        dbbr.RecordStatus = "I";
                        dbbr.ModifiedBy = dbbr.ModifiedBy;
                        dbbr.ModifiedDate = dbbr.ModifiedDate;
                        dbbr.BerthCode = dbbr.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthReasonForVisit>().Update(dbbr);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            return berthData;
        }

        private void GetValueVesselType(BerthVO berthData, List<BerthVesselType> berthVessels)
        {
            var dbberthvessels = (from bv in _unitOfWork.Repository<BerthVesselType>().Query().Select().AsEnumerable()
                                  where
                                      bv.PortCode == berthData.PortCode && bv.QuayCode == berthData.QuayCode &&
                                      bv.BerthCode == berthData.BerthCode
                                  select bv).ToList();

            if (dbberthvessels != null)
            {
                foreach (var bv in berthVessels)
                {
                    var newvessel = true;
                    foreach (var dbbv in dbberthvessels)
                    {
                        if (bv.PortCode == dbbv.PortCode && bv.QuayCode == dbbv.QuayCode && bv.BerthCode == dbbv.BerthCode
                            && bv.VesselTypeCode == dbbv.VesselTypeCode)
                        {
                            newvessel = false;
                        }
                        if (bv.PortCode == dbbv.PortCode && bv.QuayCode == dbbv.QuayCode && bv.BerthCode == dbbv.BerthCode
                            && bv.VesselTypeCode == dbbv.VesselTypeCode && dbbv.RecordStatus == "I")
                        {
                            dbbv.RecordStatus = "A";
                            dbbv.ModifiedBy = dbbv.ModifiedBy;
                            dbbv.ModifiedDate = dbbv.ModifiedDate;
                            dbbv.BerthCode = dbbv.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                            _unitOfWork.Repository<BerthVesselType>().Update(dbbv);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (newvessel)
                    {
                        bv.RecordStatus = "A";
                        bv.ObjectState = ObjectState.Added;
                        bv.BerthCode = bv.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthVesselType>().Insert(bv);
                        _unitOfWork.SaveChanges();
                    }
                }

                foreach (var dbbv in dbberthvessels)
                {
                    var inactive = true;
                    foreach (var bv in berthVessels)
                    {
                        if (dbbv.PortCode == bv.PortCode && dbbv.QuayCode == bv.QuayCode && dbbv.BerthCode == bv.BerthCode &&
                            dbbv.VesselTypeCode == bv.VesselTypeCode)
                        {
                            inactive = false;
                        }
                    }
                    if (inactive)
                    {
                        dbbv.RecordStatus = "I";
                        dbbv.ModifiedBy = dbbv.ModifiedBy;
                        dbbv.ModifiedDate = dbbv.ModifiedDate;
                        dbbv.BerthCode = dbbv.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthVesselType>().Update(dbbv);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }

        private void GetValueCargoes(BerthVO berthData, List<BerthCargo> berthCargoes)
        {
            var dbberthcargos = (from bc in _unitOfWork.Repository<BerthCargo>().Query().Select().AsEnumerable()
                                 where
                                     bc.PortCode == berthData.PortCode && bc.QuayCode == berthData.QuayCode &&
                                     bc.BerthCode == berthData.BerthCode
                                 select bc).ToList();

            if (dbberthcargos != null)
            {
                foreach (var bc in berthCargoes)
                {
                    var newcargo = true;
                    foreach (var dbbc in dbberthcargos)
                    {
                        if (bc.PortCode == dbbc.PortCode && bc.QuayCode == dbbc.QuayCode && bc.BerthCode == dbbc.BerthCode
                            && bc.CargoTypeCode == dbbc.CargoTypeCode)
                        {
                            newcargo = false;
                        }
                        if (bc.PortCode == dbbc.PortCode && bc.QuayCode == dbbc.QuayCode && bc.BerthCode == dbbc.BerthCode
                            && bc.CargoTypeCode == dbbc.CargoTypeCode && dbbc.RecordStatus == "I")
                        {
                            dbbc.RecordStatus = "A";
                            dbbc.ModifiedBy = dbbc.ModifiedBy;
                            dbbc.ModifiedDate = dbbc.ModifiedDate;
                            dbbc.BerthCode = dbbc.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                            _unitOfWork.Repository<BerthCargo>().Update(dbbc);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (newcargo)
                    {
                        bc.RecordStatus = "A";
                        bc.ObjectState = ObjectState.Added;
                        bc.BerthCode = bc.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthCargo>().Insert(bc);
                        _unitOfWork.SaveChanges();
                    }
                }

                foreach (var dbbc in dbberthcargos)
                {
                    var inactive = true;
                    foreach (var bc in berthCargoes)
                    {
                        if (dbbc.PortCode == bc.PortCode && dbbc.QuayCode == bc.QuayCode && dbbc.BerthCode == bc.BerthCode &&
                            dbbc.CargoTypeCode == bc.CargoTypeCode)
                        {
                            inactive = false;
                        }
                    }
                    if (inactive)
                    {
                        dbbc.RecordStatus = "I";
                        dbbc.ModifiedBy = dbbc.ModifiedBy;
                        dbbc.ModifiedDate = dbbc.ModifiedDate;
                        dbbc.BerthCode = dbbc.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                        _unitOfWork.Repository<BerthCargo>().Update(dbbc);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
        }

        private void GetValueModifyBerth(Berth berth)
        {
            var berthObj = _unitOfWork.Repository<Berth>().Find(berth.PortCode, berth.QuayCode, berth.BerthCode);

            if (berthObj != null)
            {
                berthObj.BerthName = berth.BerthName;
                berthObj.BerthType = berth.BerthType;
                berthObj.ShortName = berth.ShortName;
                berthObj.FromMeter = berth.FromMeter;
                berthObj.ToMeter = berth.ToMeter;
                berthObj.Draftm = berth.Draftm;
                berthObj.TidalDraft = berth.TidalDraft;
                berthObj.RecordStatus = berth.RecordStatus;
                berthObj.CreatedBy = berth.CreatedBy;
                berthObj.CreatedDate = berth.CreatedDate;
                berthObj.ModifiedBy = berth.ModifiedBy;
                berthObj.ModifiedDate = berth.ModifiedDate;

                berthObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Berth>().Update(berthObj);
                _unitOfWork.SaveChanges();
            }
        }

        #endregion

        #region DelBerthByID

        /// <summary>
        /// To Delete Berth By Id
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        public BerthVO DelBerthById(BerthVO berthData, int userId, string portCode)
        {
            if (berthData != null)
            {

                berthData.ModifiedBy = userId;
                berthData.ModifiedDate = DateTime.Now;
                berthData.PortCode = portCode;
                // }

                Berth berth = new Berth();
                berth = BerthMapExtension.MapToEntity(berthData);
                List<BerthCargo> berthCargoes = berth.BerthCargoes.ToList();
                berth.BerthCargoes = null;
                var berthObj = _unitOfWork.Repository<Berth>().Find(berth.PortCode, berth.QuayCode, berth.BerthCode);
                berthObj.RecordStatus = "I";
                berthObj.ModifiedBy = berthData.ModifiedBy;
                berthObj.ModifiedDate = berthData.ModifiedDate;
                berthObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Berth>().Update(berthObj);
                _unitOfWork.SaveChanges();

                foreach (var berthcargo in berthCargoes)
                {
                    var berthCargoObj = _unitOfWork.Repository<BerthCargo>()
                        .Find(berthData.PortCode, berthData.QuayCode, berthData.BerthCode, berthcargo.CargoTypeCode);
                    berthCargoObj.RecordStatus = "I";
                    berthCargoObj.BerthCode = berthData.BerthCode.ToUpper(CultureInfo.InvariantCulture);
                    berthObj.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<BerthCargo>().Update(berthCargoObj);
                    _unitOfWork.SaveChanges();
                }
            }
            return berthData;
        }

        
       
        #endregion
    }
}
