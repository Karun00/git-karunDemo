using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.DTOS;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class StatementFactRepository : IStatementFactRepository
    {
        private IUnitOfWork _unitOfWork;

        public StatementFactRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  To Get Statement Fact Details
        /// </summary>
        /// <returns></returns>
        public List<StatementVCNVO> StatementFactDetails(string portcode, int UserTypeID, string UserType, string vcnSearch, string vesselName)
        {
            var statementfact = new List<StatementVCNVO>();

            statementfact = GetStatementFact(portcode, UserTypeID, UserType, statementfact);

          
            foreach (var statement in statementfact)
            {
                //double duration;
                var result = (from p in _unitOfWork.Repository<StatementFactEvent>().Query().Include(p => p.SubCategory).Select()
                              where p.StatementFactID == statement.StatementFactID
                              orderby p.StatementFactEventID descending
                              select new StatementVCNVO
                              {                                
                                  StatementFactEventID = p.StatementFactEventID,
                                  DelayType = p.DelayType,
                                 // Duration=p.Duration,
                                 // Convert.ToDouble(var1.ToString("0.00"));
                                  //doublVal = Convert.ToDouble(String.Format("{0:0.00}", doublVal ));
                                  Duration = (Convert.ToDecimal(p.Duration)),
                                    //Duration=Convert.ToDecimal(duration.ToString("0.00")),  
                                  Remarks=p.Remarks,
                                  StartOperational = Convert.ToString(p.StartOperational, CultureInfo.InvariantCulture),
                                  EndOperational = Convert.ToString(p.EndOperational, CultureInfo.InvariantCulture)
                              }).ToList<StatementVCNVO>();
                statement.StatementFactEvents = result;
            }

            foreach (var statement in statementfact)
            {
                var result1 = (from p in _unitOfWork.Repository<StatementCommodity>().Queryable()
                              where p.StatementFactID == statement.StatementFactID
                              orderby p.StatementCommodityID descending
                              select new StatementCommodityVO
                              {
                                  StatementCommodityID = p.StatementCommodityID,
                                  TerminalOperatorID = p.TerminalOperatorID,
                                  PortCode = p.PortCode,
                                  QuayCode = p.QuayCode,
                                  BerthCode = p.BerthCode,
                                  CargoType = p.CargoType,
                                  Package = p.Package,
                                  UOM = p.UOM,
                                  Commodity = p.Commodity,
                                  Quantity = p.Quantity,
                                  CommodityBerthKey = p.PortCode + "." + p.QuayCode + "." + p.BerthCode
                              }).ToList<StatementCommodityVO>();
                statement.StatementCommodities = result1;
            }


            if (vcnSearch != "All")
                statementfact = statementfact.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcnSearch.ToUpperInvariant()));

            if (vesselName != "All")
                statementfact = statementfact.FindAll(t => t.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            return statementfact;
        }

        private List<StatementVCNVO> GetStatementFact(string portcode, int UserTypeID, string UserType, List<StatementVCNVO> statementfact)
        {            
            if (UserType == "AGNT")
            {
                statementfact = (from statementvo in _unitOfWork.Repository<StatementFact>().Queryable()
                                 join sc in _unitOfWork.Repository<SubCategory>().Queryable() on statementvo.OperationCode equals sc.SubCatCode
                                 join vc in _unitOfWork.Repository<VesselCall>().Queryable() on statementvo.VCN equals vc.VCN
                                 join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on vc.VCN equals an.VCN
                                 join v in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals v.VesselID

                                 join bf in _unitOfWork.Repository<Berth>().Queryable()
                                 on new { fp = vc.FromPositionPortCode, fq = vc.FromPositionQuayCode, fb = vc.FromPositionBerthCode } equals new { fp = bf.PortCode, fq = bf.QuayCode, fb = bf.BerthCode }
                                 into gj
                                 from sf1 in gj.DefaultIfEmpty()

                                 join bt in _unitOfWork.Repository<Berth>().Queryable()
                                 on new { tp = vc.ToPositionPortCode, tq = vc.ToPositionQuayCode, tb = vc.ToPositionBerthCode } equals new { tp = bt.PortCode, tq = bt.QuayCode, tb = bt.BerthCode }
                                 into gj1
                                 from sf2 in gj1.DefaultIfEmpty()


                                 where vc.FromPositionPortCode == portcode && an.AgentID == UserTypeID
                                 orderby statementvo.StatementFactID descending
                                 select new StatementVCNVO
                                 {
                                     StatementFactID = statementvo.StatementFactID,
                                     VCN = statementvo.VCN,
                                     OperationCode = statementvo.OperationCode,
                                     OperationName = sc.SubCatName,
                                     MasterName = statementvo.MasterName,
                                     ArrivalDiesel = statementvo.ArrivalDiesel,
                                     ArrivalFuel = statementvo.ArrivalFuel,
                                     SailingFuel = statementvo.SailingFuel,
                                     SailingDiesel = statementvo.SailingDiesel,

                                     VesselName = v.VesselName,
                                     VoyageIn = an.VoyageIn,
                                     VoyageOut = an.VoyageOut,


                                     SDateFrom = vc.ETB.HasValue ? vc.ETB.ToString() : null,
                                     SDateTo = vc.ETUB.HasValue ? vc.ETUB.ToString() : null,
                                     BerthFrom = sf1 != null ? sf1.BerthName : null,
                                     BerthTo = sf2 != null ? sf2.BerthName : null,  
                                     //CurrentBerth = sf1 != null && sf2 != null ? sf1.BerthName + '-' + sf2.BerthName : null,
                                    
                                     RecordStatus = statementvo.RecordStatus,
                                     CreatedBy = statementvo.CreatedBy,
                                     CreatedDate = statementvo.CreatedDate,
                                     ModifiedBy = statementvo.ModifiedBy,
                                     ModifiedDate = statementvo.ModifiedDate,

                                     EOSPDateTime = statementvo.EOSPDateTime.HasValue ? statementvo.EOSPDateTime.ToString() : null,
                                     GangwayDown = statementvo.GangwayDown.HasValue ? statementvo.GangwayDown.ToString() : null,
                                     NORTendered = statementvo.NORTendered.HasValue ? statementvo.NORTendered.ToString() : null,
                                     NORAccepted = statementvo.NORAccepted.HasValue ? statementvo.NORAccepted.ToString() : null,
                                     StevedoreOnBoard = statementvo.StevedoreOnBoard.HasValue ? statementvo.StevedoreOnBoard.ToString() : null,
                                     StevedoreStart = statementvo.StevedoreStart.HasValue ? statementvo.StevedoreStart.ToString() : null,
                                     StevedoreEnd = statementvo.StevedoreEnd.HasValue ? statementvo.StevedoreEnd.ToString() : null,
                                     StevedoreOff = statementvo.StevedoreOff.HasValue ? statementvo.StevedoreOff.ToString() : null,
                                     CranesDeployed = statementvo.CranesDeployed,
                                     StartCargo = statementvo.StartCargo.HasValue ? statementvo.StartCargo.ToString() : null,
                                     EndCargo = statementvo.EndCargo.HasValue ? statementvo.EndCargo.ToString() : null,
                                 }).ToList<StatementVCNVO>();
            }
            else
            {
                statementfact = (from statementvo in _unitOfWork.Repository<StatementFact>().Queryable()
                                 join sc in _unitOfWork.Repository<SubCategory>().Queryable() on statementvo.OperationCode equals sc.SubCatCode
                                 join vc in _unitOfWork.Repository<VesselCall>().Queryable() on statementvo.VCN equals vc.VCN
                                 join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on vc.VCN equals an.VCN
                                 join v in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals v.VesselID

                                 join bf in _unitOfWork.Repository<Berth>().Queryable()
                                 on new { fp = vc.FromPositionPortCode, fq = vc.FromPositionQuayCode, fb = vc.FromPositionBerthCode } equals new { fp = bf.PortCode, fq = bf.QuayCode, fb = bf.BerthCode }
                                 into gj
                                 from sf1 in gj.DefaultIfEmpty()

                                 join bt in _unitOfWork.Repository<Berth>().Queryable()
                                 on new { tp = vc.ToPositionPortCode, tq = vc.ToPositionQuayCode, tb = vc.ToPositionBerthCode } equals new { tp = bt.PortCode, tq = bt.QuayCode, tb = bt.BerthCode }
                                 into gj1
                                 from sf2 in gj1.DefaultIfEmpty()


                                 where vc.FromPositionPortCode == portcode // && an.AgentID == UserTypeId
                                 orderby statementvo.StatementFactID descending
                                 select new StatementVCNVO
                                 {
                                     StatementFactID = statementvo.StatementFactID,
                                     VCN = statementvo.VCN,
                                     OperationCode = statementvo.OperationCode,
                                     OperationName = sc.SubCatName,
                                     MasterName = statementvo.MasterName,
                                     ArrivalDiesel = statementvo.ArrivalDiesel,
                                     ArrivalFuel = statementvo.ArrivalFuel,
                                     SailingFuel = statementvo.SailingFuel,
                                     SailingDiesel = statementvo.SailingDiesel,

                                     VesselName = v.VesselName,
                                     VoyageIn = an.VoyageIn,
                                     VoyageOut = an.VoyageOut,
                                     
                                     SDateFrom = vc.ETB.HasValue ? vc.ETB.ToString() : null,
                                     SDateTo = vc.ETUB.HasValue ? vc.ETUB.ToString() : null,
                                     BerthFrom = sf1 != null  ? sf1.BerthName  : null,
                                     BerthTo = sf2 != null ? sf2.BerthName : null,  
                                     //CurrentBerth = sf1 != null && sf2 != null ? sf1.BerthName + '-' + sf2.BerthName : null,                                   
                                     RecordStatus = statementvo.RecordStatus,
                                     CreatedBy = statementvo.CreatedBy,
                                     CreatedDate = statementvo.CreatedDate,
                                     ModifiedBy = statementvo.ModifiedBy,
                                     ModifiedDate = statementvo.ModifiedDate,

                                     EOSPDateTime = statementvo.EOSPDateTime.HasValue ? statementvo.EOSPDateTime.ToString() : null,
                                     GangwayDown = statementvo.GangwayDown.HasValue ? statementvo.GangwayDown.ToString() : null,
                                     NORTendered = statementvo.NORTendered.HasValue ? statementvo.NORTendered.ToString() : null,
                                     NORAccepted = statementvo.NORAccepted.HasValue ? statementvo.NORAccepted.ToString() : null,
                                     StevedoreOnBoard = statementvo.StevedoreOnBoard.HasValue ? statementvo.StevedoreOnBoard.ToString() : null,
                                     StevedoreStart = statementvo.StevedoreStart.HasValue ? statementvo.StevedoreStart.ToString() : null,
                                     StevedoreEnd = statementvo.StevedoreEnd.HasValue ? statementvo.StevedoreEnd.ToString() : null,
                                     StevedoreOff = statementvo.StevedoreOff.HasValue ? statementvo.StevedoreOff.ToString() : null,
                                     CranesDeployed = statementvo.CranesDeployed,
                                     StartCargo = statementvo.StartCargo.HasValue ? statementvo.StartCargo.ToString() : null,
                                     EndCargo = statementvo.EndCargo.HasValue ? statementvo.EndCargo.ToString() : null,
                                   //  VCN_VesselName = vc.VCN + "_" + v.VesselName
                                 }).ToList<StatementVCNVO>();

            }

            foreach (var item in statementfact)
            {
                //Convert.ToDateTime(user.ValidFromDate, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                item.SDateFrom = item.SDateFrom != null ? Convert.ToDateTime(item.SDateFrom, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.SDateTo = item.SDateTo != null ? Convert.ToDateTime(item.SDateTo, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.EOSPDateTime = item.EOSPDateTime != null ? Convert.ToDateTime(item.EOSPDateTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.GangwayDown = item.GangwayDown != null ? Convert.ToDateTime(item.GangwayDown, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.NORTendered = item.NORTendered != null ? Convert.ToDateTime(item.NORTendered, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.NORAccepted = item.NORAccepted != null ? Convert.ToDateTime(item.NORAccepted, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.StevedoreOnBoard = item.StevedoreOnBoard != null ? Convert.ToDateTime(item.StevedoreOnBoard, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.StevedoreStart = item.StevedoreStart != null ? Convert.ToDateTime(item.StevedoreStart, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.StevedoreEnd = item.StevedoreEnd != null ? Convert.ToDateTime(item.StevedoreEnd, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.StevedoreOff = item.StevedoreOff != null ? Convert.ToDateTime(item.StevedoreOff, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.StartCargo = item.StartCargo != null ? Convert.ToDateTime(item.StartCargo, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.EndCargo = item.EndCargo != null ? Convert.ToDateTime(item.EndCargo, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                item.CurrentBerth = item.BerthFrom != null && item.BerthTo != null ? item.BerthFrom + '-' + item.BerthTo : null;
                item.VCN_VesselName = item.VCN + "_" + item.VesselName;

            }
            


            return statementfact;

        }


        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        public List<StatementVCNVO> GetStatementVCNS(string portcode, int UserTypeId, string UserType, string searchValue)
        {
            List<StatementVCNVO> vcns = new List<StatementVCNVO>();
            if (UserType == "AGNT") 
            {
                vcns = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.RecordStatus == "A" && vc.ATB != null)
                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable().Where(an => an.PortCode == portcode && an.AgentID == UserTypeId)   
                        on vc.VCN equals an.VCN
                        join vs in _unitOfWork.Repository<Vessel>().Queryable()
                        on an.VesselID equals vs.VesselID
                        join sf in _unitOfWork.Repository<StatementFact>().Queryable()
                        on an.VCN equals sf.VCN into gj
                        from sf1 in gj.DefaultIfEmpty()

                       // where vc.RecordStatus == "A" && an.PortCode == portcode && an.AgentID == UserTypeId && vc.ATB != null
                       // && !(from a in _unitOfWork.Repository<StatementFact>().Query().Select() select a.VCN).Contains(an.VCN) 
                        where (an.VCN.ToUpper().Contains(searchValue.ToUpper()) || vs.VesselName.ToUpper().Contains(searchValue.ToUpper()))
                        select new StatementVCNVO
                        {
                            VCN = vc.VCN,
                            VesselName = vs.VesselName,
                            VCN_VesselName = vc.VCN + "_" + vs.VesselName
                        }).OrderByDescending(an => an.VCN).ToList();
            }
            else {
                vcns = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.RecordStatus == "A" && vc.ATB != null)
                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable().Where(an => an.PortCode == portcode)
                        on vc.VCN equals an.VCN
                        join vs in _unitOfWork.Repository<Vessel>().Queryable()
                      on an.VesselID equals vs.VesselID
                        join sf in _unitOfWork.Repository<StatementFact>().Queryable()
                      on an.VCN equals sf.VCN into gj
                        from sf1 in gj.DefaultIfEmpty()

                        //  where vc.RecordStatus == "A" && an.PortCode == portcode  && vc.ATB != null
                        //    && !(from a in _unitOfWork.Repository<StatementFact>().Queryable() select a.VCN).Contains(an.VCN)
                        where (an.VCN.ToUpper().Contains(searchValue.ToUpper()) || vs.VesselName.ToUpper().Contains(searchValue.ToUpper()))
                        select new StatementVCNVO
                        {
                            VCN = vc.VCN,
                            VesselName = vs.VesselName,
                            VCN_VesselName = vc.VCN + "_" + vs.VesselName
                        }).OrderByDescending(an => an.VCN).ToList();
             
            }        

            List<StatementVCNVO> statementfacts = new List<StatementVCNVO>();

            foreach (var item in vcns)
            {
                var currentVcn = item.VCN;

                var vcnValue = (from inv in _unitOfWork.Repository<StatementFact>().Queryable().Where(inv => inv.VCN == currentVcn)
                                select new
                                {
                                    inv.VCN
                                }).ToList();

                if (vcnValue.Count == 0)
                {
                    statementfacts.Add(item);
                }
            }

            return statementfacts;
        }

        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        public StatementVCNVO GetVesselByVCN(string vcn)
        {

            //var portcode = new SqlParameter("@portcode", portCode);
            var Vcn = new SqlParameter("@VCN", vcn);

            var VCNDetails = _unitOfWork.SqlQuery<StatementVCNVO>("dbo.usp_StatementofFacts @VCN ", Vcn).FirstOrDefault();

            return VCNDetails.MapToDtoVcn();

            //var vesselinfo = (from vc in _unitOfWork.Repository<VesselCall>().Query().Select()
            //                  join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select()
            //                  on vc.VCN equals an.VCN
            //                  join vs in _unitOfWork.Repository<Vessel>().Query().Select()
            //                      on new { an.VCN, an.VesselID } equals new { VCN, vs.VesselID }
            //                  join ag in _unitOfWork.Repository<Agent>().Query().Select()
            //                      on an.AgentID equals ag.AgentID
            //                  select new StatementVCNVO
            //                  {
            //                      VCN = an.VCN,
            //                      VesselName = vs.VesselName                                

            //                  }).FirstOrDefault();     

            //  return vesselinfo;
        }

        /// <summary>
        /// To Get Entity Details Based on EntitiyCode
        /// </summary>
        /// <param name="entitycode"></param>
        /// <returns></returns>
        public Entity GetEnties(string entitycode)
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Queryable().Where(e => e.EntityCode == entitycode)
                          //where e.EntityCode == entitycode
                          select e).FirstOrDefault<Entity>();
            return entity;
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int UserID)
        {
            var users = (from u in _unitOfWork.Repository<User>().Queryable().Where(u => u.UserID == UserID)
                         //where u.UserID == UserID
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }

        /// <summary>
        /// To get notification details
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public StatementVCNVO GetStatementFactNotificationByID(string value)
        {
            var statementid = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            //var statementfact = (from sf in _unitOfWork.Repository<StatementFact>().Query()
            //                     .Include(sf => sf.ArrivalNotification)
            //                     .Include(sf => sf.ArrivalNotification.Vessel)
            //                     .Include(sf => sf.SubCategory).Select()
            //                     where sf.StatementFactID == statementid
            //                     select new StatementVCNVO
            //           {
            //               StatementFactID = sf.StatementFactID,
            //               VCN = sf.VCN,
            //               VesselName = sf.ArrivalNotification.Vessel.VesselName,
            //               OperationName = sf.SubCategory.SubCatName,
            //               MasterName = sf.MasterName

            //           }).FirstOrDefault();

            var statementfact = (from statementvo in _unitOfWork.Repository<StatementFact>().Queryable()
                                  join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on statementvo.VCN equals an.VCN
                                  join v in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals v.VesselID
                                  join sc in _unitOfWork.Repository<SubCategory>().Queryable() on statementvo.OperationCode equals sc.SubCatCode
                                  where statementvo.StatementFactID == statementid
                                     select new StatementVCNVO
                           {
                               StatementFactID = statementvo.StatementFactID,
                               VCN = statementvo.VCN,
                               VesselName = v.VesselName,
                               OperationName = sc.SubCatName,
                               MasterName = statementvo.MasterName
                           }).FirstOrDefault();
                                 


            return statementfact;
        }

        public StatementVCNVO GetTugsByVCN(string vcn)
        {


            var Vcn = new SqlParameter("@VCN", vcn);

            var TugDetails = _unitOfWork.SqlQuery<StatementTugsVO>("dbo.usp_StatementofFacts_Tugs @VCN ", Vcn).ToList();

            return TugDetails.MapToDtoTugs();


        }

    }
}
