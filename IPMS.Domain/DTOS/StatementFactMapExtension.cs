using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class StatementFactMapExtension
    {
        public static StatementFactVO MapToDto(this StatementFact data)
        {
            StatementFactVO VO = new StatementFactVO();
            if (data != null)
            {
                VO.StatementFactID = data.StatementFactID;
                VO.VCN = data.VCN;
                VO.OperationCode = data.OperationCode;
                VO.MasterName = data.MasterName;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static StatementFact MapToEntity(this StatementVCNVO vo)
        {
            StatementFact data = new StatementFact();
            if (vo != null)
            {
                data.StatementFactID = vo.StatementFactID;
                data.VCN = vo.VCN;
                data.OperationCode = vo.OperationCode;
                data.MasterName = vo.MasterName;
                data.ArrivalDiesel = vo.ArrivalDiesel;
                data.ArrivalFuel = vo.ArrivalFuel;
                data.SailingDiesel = vo.SailingDiesel;
                data.SailingFuel = vo.SailingFuel;
                data.RecordStatus = vo.RecordStatus;
                data.CranesDeployed = vo.CranesDeployed;
                if (vo.EOSPDateTime != null)
                {
                    data.EOSPDateTime = DateTime.Parse(vo.EOSPDateTime, CultureInfo.InvariantCulture);
                }
                if (vo.GangwayDown != null)
                {
                    data.GangwayDown = DateTime.Parse(vo.GangwayDown, CultureInfo.InvariantCulture);
                }
                if (vo.NORTendered != null)
                {
                    data.NORTendered = DateTime.Parse(vo.NORTendered, CultureInfo.InvariantCulture);
                }
                if (vo.NORAccepted != null)
                {
                    data.NORAccepted = DateTime.Parse(vo.NORAccepted, CultureInfo.InvariantCulture);
                }
                if (vo.StevedoreOnBoard != null)
                {
                    data.StevedoreOnBoard = DateTime.Parse(vo.StevedoreOnBoard, CultureInfo.InvariantCulture);
                }
                if (vo.StevedoreStart != null)
                {
                    data.StevedoreStart = DateTime.Parse(vo.StevedoreStart, CultureInfo.InvariantCulture);
                }
                if (vo.StevedoreEnd != null)
                {
                    data.StevedoreEnd = DateTime.Parse(vo.StevedoreEnd, CultureInfo.InvariantCulture);
                }
                if (vo.StevedoreOff != null)
                {
                    data.StevedoreOff = DateTime.Parse(vo.StevedoreOff, CultureInfo.InvariantCulture);
                }

                if (vo.StartCargo != null)
                {
                    data.StartCargo = DateTime.Parse(vo.StartCargo, CultureInfo.InvariantCulture);
                }
                if (vo.EndCargo != null)
                {
                    data.EndCargo = DateTime.Parse(vo.EndCargo, CultureInfo.InvariantCulture);
                }

                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
                data.StatementFactEvents = vo.StatementFactEvents.ToList().MapToEntity();
                data.StatementCommodities = vo.StatementCommodities.ToList().MapToEntityCommodities();
            }

            return data;
        }

        public static StatementFact MapToEntityStatement(this StatementVCNVO vo)
        {
            StatementFact data = new StatementFact();
            if (vo != null)
            {
                data.StatementFactID = vo.StatementFactID;
                data.VCN = vo.VCN;
                data.OperationCode = vo.OperationCode;
                data.MasterName = vo.MasterName;
                data.ArrivalDiesel = vo.ArrivalDiesel;
                data.ArrivalFuel = vo.ArrivalFuel;
                data.SailingDiesel = vo.SailingDiesel;
                data.SailingFuel = vo.SailingFuel;
                data.RecordStatus = vo.RecordStatus;       

                List<StatementFactEvent> statementevents = new List<StatementFactEvent>();
                foreach (var statementevent in vo.StatementFactEvents)
                {
                    StatementFactEvent obj = new StatementFactEvent();
                    obj.StatementFactID = vo.StatementFactID;
                    obj.StatementFactEventID = statementevent.StatementFactEventID;
                    obj.RecordStatus = vo.RecordStatus;
                    obj.CreatedBy = vo.CreatedBy;
                    obj.CreatedDate = vo.CreatedDate;
                    obj.ModifiedBy = vo.ModifiedBy;
                    obj.ModifiedDate = vo.ModifiedDate;
                    statementevents.Add(obj);
                }

                data.StatementFactEvents = statementevents;

                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
            }

            return data;
        }

        public static StatementVCNVO MapToDtoVcn(this StatementVCNVO vo)
        {
            StatementVCNVO vcndetailvo = new StatementVCNVO();
            if (vo != null)
            {
                vcndetailvo.VCN = vo.VCN;
                vcndetailvo.VesselName = vo.VesselName;
                vcndetailvo.CurrentBerth = vo.BerthFrom + '-' + vo.BerthTo;
                // vcndetailvo.DateFrom = vcndetail.DateFrom;
                // vcndetailvo.DateTo = vcndetail.DateTo;
                vcndetailvo.VoyageIn = vo.VoyageIn;
                vcndetailvo.VoyageOut = vo.VoyageOut;

                vcndetailvo.InwardPilotOnBoard = vo.InwardPilotOnBoard;
                vcndetailvo.InwardPilotAway = vo.InwardPilotAway;
                vcndetailvo.InwardFirstLine = vo.InwardFirstLine;
                vcndetailvo.InwardAllFast = vo.InwardAllFast;

                vcndetailvo.OutwardPilotOnBoard = vo.OutwardPilotOnBoard;
                vcndetailvo.OutwardPilotAway = vo.OutwardPilotAway;
                vcndetailvo.OutwardAllCast = vo.OutwardAllCast;

                vcndetailvo.DraftArrivalFwd = vo.DraftArrivalFwd;
                vcndetailvo.DraftArrivalAft = vo.DraftArrivalAft;
                vcndetailvo.DraftSailingAft = vo.DraftSailingAft;
                vcndetailvo.DraftSailingFwd = vo.DraftSailingFwd;

                vcndetailvo.SDateFrom = vo.DateFrom != null ? vo.DateFrom.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
                vcndetailvo.SDateTo = vo.DateTo != null ? vo.DateTo.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
            }
            return vcndetailvo;
        }


        public static StatementVCNVO MapToDtoTugs(this List<StatementTugsVO> tugs)
        {
            StatementVCNVO sv = new StatementVCNVO();
            if (tugs != null)
            {
                sv.SailingDetails = tugs.FindAll(t => t.MovementType == "SGMV");
                sv.ArrivalDetails = tugs.FindAll(t => t.MovementType == "ARMV");
            }
            return sv;
        }


        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ICollection<StatementFactEvent> MapToEntity(this List<StatementVCNVO> data)
        {
            ICollection<StatementFactEvent> vos = new List<StatementFactEvent>();
            if (data != null)
            {
                foreach (var statementevent in data)
                {
                    vos.Add(statementevent.MapToEntityEvent());
                }
            }
            return vos;
        }


        public static StatementFactEvent MapToEntityEvent(this StatementVCNVO vo)
        {
            StatementFactEvent data = new StatementFactEvent();
            if (vo != null)
            {
                data.StatementFactID = vo.StatementFactID;
                data.StatementFactEventID = vo.StatementFactEventID;
                data.DelayType = vo.DelayType;
                if (vo.StartOperational != null)
                {
                    data.StartOperational = DateTime.Parse(vo.StartOperational,CultureInfo.InvariantCulture);
                }
                if (vo.Duration != null)
                {
                    data.Duration = vo.Duration;
                }
                if (vo.Remarks != null)
                {
                    data.Remarks = vo.Remarks;
                }
                if (vo.EndOperational != null)
                {
                    data.EndOperational = DateTime.Parse(vo.EndOperational,CultureInfo.InvariantCulture);
                }
                data.RecordStatus = vo.RecordStatus;
            }
            return data;
        }


        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ICollection<StatementCommodity> MapToEntityCommodities(this List<StatementCommodityVO> data)
        {
            ICollection<StatementCommodity> vos = new List<StatementCommodity>();
            if (data != null)
            {
                foreach (var statementevent in data)
                {
                    vos.Add(statementevent.MapToEntityCommodity());
                }
            }
            return vos;
        }


        public static StatementCommodity MapToEntityCommodity(this StatementCommodityVO vo)
        {
            string[] fields = vo.CommodityBerthKey.Split('.');

            string portCode = fields[0];
            string quayCode = fields[1];
            string berthCode = fields[2];

            StatementCommodity data = new StatementCommodity();
            if (vo != null)
            {
                data.StatementFactID = vo.StatementFactID;
                data.StatementCommodityID = vo.StatementCommodityID;
                data.TerminalOperatorID = vo.TerminalOperatorID;
                data.PortCode = portCode;
                data.QuayCode = quayCode;
                data.BerthCode = berthCode;
                data.CargoType = vo.CargoType;
                data.Package = vo.Package;
                data.UOM = vo.UOM;
                data.Commodity = vo.Commodity;
                data.Quantity = vo.Quantity;             
                data.RecordStatus = vo.RecordStatus;
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
            }
            return data;
        }

    }
}
