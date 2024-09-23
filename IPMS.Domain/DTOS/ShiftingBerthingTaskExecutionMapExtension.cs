using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ShiftingBerthingTaskExecutionMapExtension
    {
        public static ShiftingBerthingTaskExecutionVO MapToDTO(this ShiftingBerthingTaskExecution data)
        {
            ShiftingBerthingTaskExecutionVO SBTExecutionVO = new ShiftingBerthingTaskExecutionVO();
            SBTExecutionVO.BerthingTaskExecutionID = data.BerthingTaskExecutionID;
            SBTExecutionVO.ResourceAllocationID = data.ResourceAllocationID;
            SBTExecutionVO.StartTime = data.StartTime;
            SBTExecutionVO.EndTime = data.EndTime;

            SBTExecutionVO.FromBerthPortCode = data.FromBerthPortCode;
            SBTExecutionVO.FromBerthQuayCode = data.FromBerthQuayCode;
            SBTExecutionVO.FromBerthCode = data.FromBerthCode;
            SBTExecutionVO.FromBerthKey = data.FromBerthPortCode + "." + data.FromBerthQuayCode + "." + data.FromBerthCode;

            SBTExecutionVO.ToBerthPortCode = data.ToBerthPortCode;
            SBTExecutionVO.ToBerthQuayCode = data.ToBerthQuayCode;
            SBTExecutionVO.ToBerthCode = data.ToBerthCode;
            SBTExecutionVO.ToBerthKey = data.ToBerthPortCode + "." + data.ToBerthQuayCode + "." + data.ToBerthCode;


            SBTExecutionVO.BerthingSide = data.BerthingSide;

            SBTExecutionVO.FromBollardPortCode = data.FromBollardPortCode;
            SBTExecutionVO.FromBollardQuayCode = data.FromBollardQuayCode;
            SBTExecutionVO.FromBollardBerthCode = data.FromBollardBerthCode;
            SBTExecutionVO.FromBollardCode = data.FromBollardCode;
            SBTExecutionVO.FromBolardKey = data.FromBollardPortCode + "." + data.FromBollardQuayCode + "." + data.FromBollardBerthCode + "." + data.FromBollardCode;



            SBTExecutionVO.ToBollardPortCode = data.ToBollardPortCode;
            SBTExecutionVO.ToBollardQuayCode = data.ToBollardQuayCode;
            SBTExecutionVO.ToBollardBerthCode = data.ToBollardBerthCode;
            SBTExecutionVO.ToBollardCode = data.ToBollardCode;

            SBTExecutionVO.ToBolardKey = data.ToBollardPortCode + "." + data.ToBollardQuayCode + "." + data.ToBollardBerthCode + "." + data.ToBollardCode;




            SBTExecutionVO.MooringBollardBowPortcode = data.MooringBollardBowPortcode;
            SBTExecutionVO.MooringBollardBowQuayCode = data.MooringBollardBowQuayCode;
            SBTExecutionVO.MooringBollardBowBerthCode = data.MooringBollardBowBerthCode;
            SBTExecutionVO.MooringBollardBowBollardCode = data.MooringBollardBowBollardCode;
            SBTExecutionVO.MooringBolardBowKey = data.MooringBollardBowPortcode + "." + data.MooringBollardBowQuayCode + "." + data.MooringBollardBowBerthCode + "." + data.MooringBollardBowBollardCode;

            SBTExecutionVO.MooringBollardStemPortcode = data.MooringBollardStemPortcode;
            SBTExecutionVO.MooringBollardStemQuayCode = data.MooringBollardStemQuayCode;
            SBTExecutionVO.MooringBollardStemBerthCode = data.MooringBollardStemBerthCode;
            SBTExecutionVO.MooringBollardStemBollardCode = data.MooringBollardStemBollardCode;
            SBTExecutionVO.MooringBolardStemKey = data.MooringBollardStemPortcode + "." + data.MooringBollardStemQuayCode + "." + data.MooringBollardStemBerthCode + "." + data.MooringBollardStemBollardCode;



            SBTExecutionVO.FirstLineIn = data.FirstLineIn;
            SBTExecutionVO.LastLineIn = data.LastLineIn;
            SBTExecutionVO.FirstLineOut = data.FirstLineOut;
            SBTExecutionVO.LastLineOut = data.LastLineOut;
            SBTExecutionVO.ForwardDraft = data.ForwardDraft;
            SBTExecutionVO.AftDraft = data.AftDraft;
            SBTExecutionVO.Remarks = data.Remarks;
            SBTExecutionVO.Deficiencies = data.Deficiencies;
            SBTExecutionVO.AftDraft = data.AftDraft;
            SBTExecutionVO.RecordStatus = data.RecordStatus;
            SBTExecutionVO.CreatedBy = data.CreatedBy;
            SBTExecutionVO.CreatedDate = data.CreatedDate;
            SBTExecutionVO.ModifiedBy = data.ModifiedBy;
            SBTExecutionVO.ModifiedDate = data.ModifiedDate;
            SBTExecutionVO.WaitingStartTime = data.WaitingStartTime;
            SBTExecutionVO.WaitingEndTime = data.WaitingEndTime;
            SBTExecutionVO.DelayReason = data.DelayReason != null ? data.DelayReason : "";
            SBTExecutionVO.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
            return SBTExecutionVO;
        }
        public static ShiftingBerthingTaskExecution MapToEntity(this ShiftingBerthingTaskExecutionVO vo)
        {
            ShiftingBerthingTaskExecution SBTExecution = new ShiftingBerthingTaskExecution();
            SBTExecution.BerthingTaskExecutionID = vo.BerthingTaskExecutionID;
            SBTExecution.ResourceAllocationID = vo.ResourceAllocationID;
            SBTExecution.StartTime = vo.StartTime;
            SBTExecution.EndTime = vo.EndTime;


            if ((!string.IsNullOrEmpty(vo.FromBerthKey)) && (vo.FromBerthKey != "undefined"))
            {

                string[] fields = vo.FromBerthKey.Split('.');
                string portCode = fields[0];
                string quayCode = fields[1];
                string berthCode = fields[2];
                SBTExecution.FromBerthPortCode = portCode;
                SBTExecution.FromBerthQuayCode = quayCode;
                SBTExecution.FromBerthCode = berthCode;


            }

            if ((!string.IsNullOrEmpty(vo.ToBerthKey)) && (vo.ToBerthKey != "undefined"))
            {
                string[] fields1 = vo.ToBerthKey.Split('.');
                string toportCode = fields1[0];
                string toquayCode = fields1[1];
                string toberthCode = fields1[2];
                SBTExecution.ToBerthPortCode = toportCode;
                SBTExecution.ToBerthQuayCode = toquayCode;
                SBTExecution.ToBerthCode = toberthCode;
            }

            if ((!string.IsNullOrEmpty(vo.BerthingSide)) && (vo.BerthingSide != "undefined"))
            {
                SBTExecution.BerthingSide = vo.BerthingSide;
            }

            if ((!string.IsNullOrEmpty(vo.FromBolardKey)) && (vo.FromBolardKey != "undefined"))
            {
                string[] fields2 = vo.FromBolardKey.Split('.');
                string frmBlrdportCode = fields2[0];
                string frmBlrdquayCode = fields2[1];
                string frmBlrdberthCode = fields2[2];
                string frmBollardCode = fields2[3];
                SBTExecution.FromBollardPortCode = frmBlrdportCode;
                SBTExecution.FromBollardQuayCode = frmBlrdquayCode;
                SBTExecution.FromBollardBerthCode = frmBlrdberthCode;
                SBTExecution.FromBollardCode = frmBollardCode;
            }


            if ((!string.IsNullOrEmpty(vo.ToBolardKey)) && (vo.ToBolardKey != "undefined"))
            {
                string[] fields3 = vo.ToBolardKey.Split('.');
                string toBlrdportCode = fields3[0];
                string toBlrdquayCode = fields3[1];
                string toBlrdberthCode = fields3[2];
                string toBollardCode = fields3[3];
                SBTExecution.ToBollardPortCode = toBlrdportCode;
                SBTExecution.ToBollardQuayCode = toBlrdquayCode;
                SBTExecution.ToBollardBerthCode = toBlrdberthCode;
                SBTExecution.ToBollardCode = toBollardCode;
            }

            if ((!string.IsNullOrEmpty(vo.MooringBolardBowKey)) && (vo.MooringBolardBowKey != "undefined"))
            {
                string[] fields4 = vo.MooringBolardBowKey.Split('.');
                string MoBowBlrdportCode = fields4[0];
                string MoBowBlrdquayCode = fields4[1];
                string MoBowBlrdberthCode = fields4[2];
                string MoBowBollardCode = fields4[3];
                SBTExecution.MooringBollardBowPortcode = MoBowBlrdportCode;
                SBTExecution.MooringBollardBowQuayCode = MoBowBlrdquayCode;
                SBTExecution.MooringBollardBowBerthCode = MoBowBlrdberthCode;
                SBTExecution.MooringBollardBowBollardCode = MoBowBollardCode;
            }

            if ((!string.IsNullOrEmpty(vo.MooringBolardStemKey)) && (vo.MooringBolardStemKey != "undefined"))
            {
                string[] fields5 = vo.MooringBolardStemKey.Split('.');
                string MostrnBlrdportCode = fields5[0];
                string MostrnBlrdquayCode = fields5[1];
                string MostrnBlrdberthCode = fields5[2];
                string MostrnBollardCode = fields5[3];
                SBTExecution.MooringBollardStemPortcode = MostrnBlrdportCode;
                SBTExecution.MooringBollardStemQuayCode = MostrnBlrdquayCode;
                SBTExecution.MooringBollardStemBerthCode = MostrnBlrdberthCode;
                SBTExecution.MooringBollardStemBollardCode = MostrnBollardCode;
            }

            SBTExecution.FirstLineIn = vo.FirstLineIn;
            SBTExecution.LastLineIn = vo.LastLineIn;
            SBTExecution.FirstLineOut = vo.FirstLineOut;
            SBTExecution.LastLineOut = vo.LastLineOut;
            SBTExecution.ForwardDraft = vo.ForwardDraft;
            SBTExecution.AftDraft = vo.AftDraft;
            SBTExecution.Remarks = vo.Remarks;
            SBTExecution.Deficiencies = vo.Deficiencies;
            SBTExecution.AftDraft = vo.AftDraft;
            SBTExecution.RecordStatus = vo.RecordStatus;
            SBTExecution.CreatedBy = vo.CreatedBy;
            SBTExecution.CreatedDate = vo.CreatedDate;
            SBTExecution.ModifiedBy = vo.ModifiedBy;
            SBTExecution.ModifiedDate = vo.ModifiedDate;
            SBTExecution.WaitingStartTime = vo.WaitingStartTime;
            SBTExecution.WaitingEndTime = vo.WaitingEndTime;
            SBTExecution.DelayReason = vo.DelayReason != null ? vo.DelayReason : "";
            SBTExecution.DelayOtherReason = vo.DelayOtherReason != null ? vo.DelayOtherReason : "";
            return SBTExecution;
        }

        public static List<ShiftingBerthingTaskExecution> MapToEntity(this List<ShiftingBerthingTaskExecutionVO> vos)
        {
            List<ShiftingBerthingTaskExecution> SBTExecutionEntities = new List<ShiftingBerthingTaskExecution>();
            foreach (var sbtevo in vos)
            {
                SBTExecutionEntities.Add(sbtevo.MapToEntity());
            }
            return SBTExecutionEntities;
        }
        public static List<ShiftingBerthingTaskExecutionVO> MapToDTO(this IEnumerable<ShiftingBerthingTaskExecution> entities)
        {
            List<ShiftingBerthingTaskExecutionVO> SBTExecutionvos = new List<ShiftingBerthingTaskExecutionVO>();
            foreach (var entity in entities)
            {
                SBTExecutionvos.Add(entity.MapToDTO());
            }
            return SBTExecutionvos;
        }


        public static ShiftingBerthingTaskExecutionVO MapToDTOObj(this IEnumerable<ShiftingBerthingTaskExecution> entities)
        {
            //var bunkeringVoList = new ShiftingBerthingTaskExecution();
            //     BunkeringVO BunkeringVO = new BunkeringVO();
            ShiftingBerthingTaskExecutionVO SBTExecutionVO = new ShiftingBerthingTaskExecutionVO();
            foreach (var data in entities)
            {
                SBTExecutionVO.BerthingTaskExecutionID = data.BerthingTaskExecutionID;
                SBTExecutionVO.ResourceAllocationID = data.ResourceAllocationID;
                SBTExecutionVO.StartTime = data.StartTime;
                SBTExecutionVO.EndTime = data.EndTime;

                SBTExecutionVO.FromBerthPortCode = data.FromBerthPortCode;
                SBTExecutionVO.FromBerthQuayCode = data.FromBerthQuayCode;
                SBTExecutionVO.FromBerthCode = data.FromBerthCode;
                SBTExecutionVO.FromBerthKey = data.FromBerthPortCode + "." + data.FromBerthQuayCode + "." + data.FromBerthCode;

                SBTExecutionVO.ToBerthPortCode = data.ToBerthPortCode;
                SBTExecutionVO.ToBerthQuayCode = data.ToBerthQuayCode;
                SBTExecutionVO.ToBerthCode = data.ToBerthCode;
                SBTExecutionVO.ToBerthKey = data.ToBerthPortCode + "." + data.ToBerthQuayCode + "." + data.ToBerthCode;


                SBTExecutionVO.BerthingSide = data.BerthingSide;

                SBTExecutionVO.FromBollardPortCode = data.FromBollardPortCode;
                SBTExecutionVO.FromBollardQuayCode = data.FromBollardQuayCode;
                SBTExecutionVO.FromBollardBerthCode = data.FromBollardBerthCode;
                SBTExecutionVO.FromBollardCode = data.FromBollardCode;
                SBTExecutionVO.FromBolardKey = data.FromBollardPortCode + "." + data.FromBollardQuayCode + "." + data.FromBollardBerthCode + "." + data.FromBollardCode;



                SBTExecutionVO.ToBollardPortCode = data.ToBollardPortCode;
                SBTExecutionVO.ToBollardQuayCode = data.ToBollardQuayCode;
                SBTExecutionVO.ToBollardBerthCode = data.ToBollardBerthCode;
                SBTExecutionVO.ToBollardCode = data.ToBollardCode;

                SBTExecutionVO.ToBolardKey = data.ToBollardPortCode + "." + data.ToBollardQuayCode + "." + data.ToBollardBerthCode + "." + data.ToBollardCode;




                SBTExecutionVO.MooringBollardBowPortcode = data.MooringBollardBowPortcode;
                SBTExecutionVO.MooringBollardBowQuayCode = data.MooringBollardBowQuayCode;
                SBTExecutionVO.MooringBollardBowBerthCode = data.MooringBollardBowBerthCode;
                SBTExecutionVO.MooringBollardBowBollardCode = data.MooringBollardBowBollardCode;
                SBTExecutionVO.MooringBolardBowKey = data.MooringBollardBowPortcode + "." + data.MooringBollardBowQuayCode + "." + data.MooringBollardBowBerthCode + "." + data.MooringBollardBowBollardCode;

                SBTExecutionVO.MooringBollardStemPortcode = data.MooringBollardStemPortcode;
                SBTExecutionVO.MooringBollardStemQuayCode = data.MooringBollardStemQuayCode;
                SBTExecutionVO.MooringBollardStemBerthCode = data.MooringBollardStemBerthCode;
                SBTExecutionVO.MooringBollardStemBollardCode = data.MooringBollardStemBollardCode;
                SBTExecutionVO.MooringBolardStemKey = data.MooringBollardStemPortcode + "." + data.MooringBollardStemQuayCode + "." + data.MooringBollardStemBerthCode + "." + data.MooringBollardStemBollardCode;



                SBTExecutionVO.FirstLineIn = data.FirstLineIn;
                SBTExecutionVO.LastLineIn = data.LastLineIn;
                SBTExecutionVO.FirstLineOut = data.FirstLineOut;
                SBTExecutionVO.LastLineOut = data.LastLineOut;
                SBTExecutionVO.ForwardDraft = data.ForwardDraft;
                SBTExecutionVO.AftDraft = data.AftDraft;
                SBTExecutionVO.Remarks = data.Remarks;
                SBTExecutionVO.Deficiencies = data.Deficiencies;
                SBTExecutionVO.AftDraft = data.AftDraft;
                SBTExecutionVO.RecordStatus = data.RecordStatus;
                SBTExecutionVO.CreatedBy = data.CreatedBy;
                SBTExecutionVO.CreatedDate = data.CreatedDate;
                SBTExecutionVO.ModifiedBy = data.ModifiedBy;
                SBTExecutionVO.ModifiedDate = data.ModifiedDate;
                SBTExecutionVO.WaitingStartTime = data.WaitingStartTime;
                SBTExecutionVO.WaitingEndTime = data.WaitingEndTime;
                SBTExecutionVO.DelayReason = data.DelayReason != null ? data.DelayReason : "";
                SBTExecutionVO.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
            }
            return SBTExecutionVO;
        }
    }
}
