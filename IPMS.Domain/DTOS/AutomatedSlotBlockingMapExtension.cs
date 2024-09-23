using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class AutomatedSlotBlockingMapExtension
    {
        public static List<AutomatedSlotBlocking> MapToEntity(this List<AutomatedSlotBlockingVO> vos)
        {
            List<AutomatedSlotBlocking> autoslot = new List<AutomatedSlotBlocking>();
            if (vos != null)
            {
                foreach (var stvo in vos)
                {
                    autoslot.Add(stvo.MapToEntity());
                }
            }
            return autoslot;
        }

        public static List<AutomatedSlotBlockingVO> MapToDto(this List<AutomatedSlotBlocking> entities)
        {
            List<AutomatedSlotBlockingVO> autoslotlist = new List<AutomatedSlotBlockingVO>();
            if (entities != null)
            {
                foreach (var stvo in entities)
                {
                    autoslotlist.Add(stvo.MapToDto());
                }
            }
            return autoslotlist;
        }

        public static AutomatedSlotBlocking MapToEntity(this AutomatedSlotBlockingVO vo)
        {
            AutomatedSlotBlocking autoslot = new AutomatedSlotBlocking();
            if (vo != null)
            {
                autoslot.AutomatedSlotBlockingId = vo.AutomatedSlotBlockingId;
                autoslot.FromDate = Convert.ToDateTime(vo.FromDate, CultureInfo.InvariantCulture);
                autoslot.ToDate = Convert.ToDateTime(vo.ToDate, CultureInfo.InvariantCulture);
                autoslot.SlotFrom = vo.SlotFrom;
                autoslot.SlotTo = vo.SlotTo;
                autoslot.Remarks = vo.Remarks;
                autoslot.PortCode = vo.PortCode;
                autoslot.Reason = vo.Reason;
                autoslot.RecordStatus = vo.RecordStatus;                
                autoslot.TotalSlots = vo.TotalSlots;
                autoslot.Other = vo.Other;
                autoslot.CreatedBy = vo.CreatedBy;
                autoslot.CreatedDate = vo.CreatedDate;
                autoslot.ModifiedBy = vo.ModifiedBy;
                autoslot.ModifiedDate = vo.ModifiedDate;
            }
            return autoslot;
        }

        public static AutomatedSlotBlockingVO MapToDto(this AutomatedSlotBlocking data)
        {
            AutomatedSlotBlockingVO vo = new AutomatedSlotBlockingVO();
            if (data != null)
            {
                vo.AutomatedSlotBlockingId = data.AutomatedSlotBlockingId;
                vo.FromDate = data.FromDate.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                vo.ToDate = data.ToDate.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                vo.SlotFromDate = data.FromDate;
                vo.SlotToDate = data.ToDate;

                vo.SlotFrom = data.SlotFrom;
                vo.SlotTo = data.SlotTo;
                
                string[] startSlot = data.SlotFrom != null ? data.SlotFrom.Split('-') : null;

                string[] endSlot = data.SlotTo != null ? data.SlotTo.Split('-') : null;

                DateTime sttime = Convert.ToDateTime(startSlot[0], CultureInfo.InvariantCulture);

                DateTime edtime = Convert.ToDateTime(endSlot[1], CultureInfo.InvariantCulture);
                
                vo.StartTime = sttime.TimeOfDay.TotalMinutes;
                vo.EndTime = edtime.TimeOfDay.TotalMinutes;



                DateTime SlotToDate1 = vo.SlotToDate; 

                DateTime edStarttime = Convert.ToDateTime(endSlot[0], CultureInfo.InvariantCulture);

                var endMinutes = edStarttime.TimeOfDay.TotalMinutes;
                if (vo.EndTime <= endMinutes)
                {
                    SlotToDate1 = SlotToDate1.AddDays(1);
                }
                else
                {
                    SlotToDate1 = vo.SlotToDate;
                }

                DateTime currentDate = DateTime.Now;
                DateTime endTime = SlotToDate1.Date.AddHours(0).AddMinutes(vo.EndTime).AddSeconds(0);

                if (currentDate <= endTime)
                {
                    vo.EditVisible = true;
                }
                else
                {
                    vo.EditVisible = false;
                }

                vo.ToStartTime = endMinutes;
                vo.ReasonName = data.SubCategory != null ? data.SubCategory.SubCatName : "";               
                vo.Other = data.Other;
                vo.Remarks = data.Remarks;
                vo.PortCode = data.PortCode;
                vo.Reason = data.Reason;
                if (vo.Reason == "ROTR")
                {
                    vo.ReasonName = data.Other;
                }
                vo.TotalSlots = data.TotalSlots;
                vo.RecordStatus = data.RecordStatus;
                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = data.ModifiedDate;
             
            }
            return vo;
        }
    }
}
