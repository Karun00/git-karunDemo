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
    public static class VesselCallAnchorageMapExtension
    {
        public static List<VesselCallAnchorageVO> MapToDTO(this List<VesselCallAnchorage> data)
        {
            List<VesselCallAnchorageVO> vos = new List<VesselCallAnchorageVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDTO());
                }
            }
            return vos;
        }

        public static VesselCallAnchorageVO MapToDTO(this VesselCallAnchorage data)
        {
            VesselCallAnchorageVO vesselcallanchorageVo = new VesselCallAnchorageVO();
            if (data != null)
            {
                vesselcallanchorageVo.VCN = data.VCN;
                vesselcallanchorageVo.VesselCallAnchorageID = data.VesselCallAnchorageID;
                vesselcallanchorageVo.AnchorAweighTime = Convert.ToString(data.AnchorAweighTime,CultureInfo.InvariantCulture);
                vesselcallanchorageVo.AnchorDropTime = Convert.ToString(data.AnchorDropTime,CultureInfo.InvariantCulture);
                vesselcallanchorageVo.AnchorPosition = data.AnchorPosition;
                vesselcallanchorageVo.BearingDistanceFromBreakWater = data.BearingDistanceFromBreakWater;
                vesselcallanchorageVo.Reason = data.Reason;
                vesselcallanchorageVo.Remarks = data.Remarks;
                vesselcallanchorageVo.RecordStatus = data.RecordStatus;
                vesselcallanchorageVo.CreatedBy = data.CreatedBy;
                vesselcallanchorageVo.CreatedDate = data.CreatedDate;
                vesselcallanchorageVo.ModifiedBy = data.ModifiedBy;
                vesselcallanchorageVo.ModifiedDate = data.ModifiedDate;
            }
            return vesselcallanchorageVo;
        }       

        public static VesselCallAnchorage MapToEntity(this VesselCallAnchorageVO vo)
        {
            VesselCallAnchorage vesselcallanchorage = new VesselCallAnchorage();
            if (vo != null)
            {
                vesselcallanchorage.VesselCallAnchorageID = vo.VesselCallAnchorageID;
                vesselcallanchorage.VCN = vo.VCN;

                if (vo.AnchorAweighTime != null)
                {
                    if (!string.IsNullOrWhiteSpace(vo.AnchorAweighTime))
                    {
                        vesselcallanchorage.AnchorAweighTime = DateTime.Parse(vo.AnchorAweighTime,CultureInfo.InvariantCulture);
                    }
                }

                if (vo.AnchorDropTime != null)
                {
                    if (!string.IsNullOrWhiteSpace(vo.AnchorDropTime))
                    {
                        vesselcallanchorage.AnchorDropTime = DateTime.Parse(vo.AnchorDropTime,CultureInfo.InvariantCulture);
                    }
                }

                vesselcallanchorage.AnchorPosition = vo.AnchorPosition;
                vesselcallanchorage.BearingDistanceFromBreakWater = vo.BearingDistanceFromBreakWater;
                vesselcallanchorage.Reason = vo.Reason;
                vesselcallanchorage.Remarks = vo.Remarks;
                vesselcallanchorage.RecordStatus = vo.RecordStatus;
                vesselcallanchorage.CreatedBy = vo.CreatedBy;
                vesselcallanchorage.CreatedDate = vo.CreatedDate;
                vesselcallanchorage.ModifiedBy = vo.ModifiedBy;
                vesselcallanchorage.ModifiedDate = vo.ModifiedDate;
            }
            return vesselcallanchorage;
        }

        public static List<VesselCallAnchorage> MapToEntity(this List<VesselCallAnchorageVO> data)
        {
            List<VesselCallAnchorage> vos = new List<VesselCallAnchorage>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToEntity());
                }
            }
            return vos;
        }
    }
}

