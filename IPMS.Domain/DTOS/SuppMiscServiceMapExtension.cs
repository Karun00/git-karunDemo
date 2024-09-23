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
    public static class SuppMiscServiceMapExtension
    {
        public static List<SuppMiscServiceVO> MapToDto(this List<SuppMiscService> suppMisc)
        {
            List<SuppMiscServiceVO> SuppMiscVOs = new List<SuppMiscServiceVO>();

            if (suppMisc != null)
            {
                foreach (SuppMiscService obj in suppMisc)
                {
                    SuppMiscVOs.Add(obj.MapToDto());
                }
            }

            return SuppMiscVOs;
        }

        public static SuppMiscServiceVO MapToDto(this SuppMiscService data)
        {
            SuppMiscServiceVO VO = new SuppMiscServiceVO();
            if (data != null)
            {
                VO.SuppMiscServiceID = data.SuppMiscServiceID;
                VO.SuppDryDockID = data.SuppDryDockID;
                VO.ServiceTypeID = data.ServiceTypeID;
                VO.Phase = data.Phase;
                VO.FromDateTime = data.FromDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                VO.ToDateTime = data.ToDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                VO.Quantity = data.Quantity;
                VO.StartMeterReading = data.StartMeterReading;//added by divya on 30COt2017
                VO.EndMeterReading = data.EndMeterReading;//added by divya on 30COt2017                   
                VO.Remarks = data.Remarks;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.ServiceTypeCode = data.ServiceTypeCode;

                VO.VCN = data.SuppDryDock.VCN;
                VO.VesselName = data.SuppDryDock.ArrivalNotification != null ? (data.SuppDryDock.ArrivalNotification.Vessel != null ? data.SuppDryDock.ArrivalNotification.Vessel.VesselName : string.Empty) : string.Empty;
                VO.VesselAgent = data.SuppDryDock.ArrivalNotification != null ? (data.SuppDryDock.ArrivalNotification.Agent != null ? data.SuppDryDock.ArrivalNotification.Agent.RegisteredName : string.Empty) : string.Empty;
                VO.RequestFromDate = data.SuppDryDock.FromDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                VO.RequestToDate = data.SuppDryDock.ToDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            return VO;
        }


        public static SuppMiscService MapToEntity(this SuppMiscServiceVO vo)
        {
            SuppMiscService data = new SuppMiscService();
            if (vo != null)
            {
                data.SuppMiscServiceID = vo.SuppMiscServiceID;
                data.SuppDryDockID = vo.SuppDryDockID;
                data.ServiceTypeID = vo.ServiceTypeID;
                data.Phase = vo.Phase;
                data.FromDateTime = Convert.ToDateTime(vo.FromDateTime, CultureInfo.InvariantCulture);
                data.ToDateTime = Convert.ToDateTime(vo.ToDateTime, CultureInfo.InvariantCulture);
                data.StartMeterReading=vo.StartMeterReading;//added by divya on 30COt2017
                data.EndMeterReading=vo.EndMeterReading;//added by divya on 30COt2017
                data.Quantity = vo.Quantity;
                data.ServiceTypeCode = vo.ServiceTypeCode;              
                data.Remarks = vo.Remarks;
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
