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
    public static class VesselCallMapExtenstion
    {
        public static VesselCall MapToEntity(this VesselCallVO vo)
        {
            VesselCall vesselcall = new VesselCall();
            if (vo != null)
            {
                vesselcall.VesselCallID = vo.VesselCallID;
                vesselcall.RecentAgentID = vo.RecentAgentID;
                vesselcall.ETA = vo.ETA;
                vesselcall.ETD = vo.ETD;
                if (vo.ATB != null)
                {
                    vesselcall.ATB = vo.ATB;
                }
                if (vo.ATUB != null)
                {
                    vesselcall.ATUB = vo.ATUB;
                }


                if (vo.ATA != null)
                {
                    if (!string.IsNullOrWhiteSpace(vo.ATA))
                    {
                        vesselcall.ATA = DateTime.Parse(vo.ATA, CultureInfo.InvariantCulture);
                    }
                }

                if (vo.ATD != null)
                {
                    if (!string.IsNullOrWhiteSpace(vo.ATD))
                    {
                        vesselcall.ATD = DateTime.Parse(vo.ATD, CultureInfo.InvariantCulture);
                    }
                }
                vesselcall.BreakWaterIn = string.IsNullOrEmpty(vo.BreakWaterIn) ? DateTime.MinValue : Convert.ToDateTime(vo.BreakWaterIn, CultureInfo.InvariantCulture);
                vesselcall.BreakWaterOut = string.IsNullOrEmpty(vo.BreakWaterOut) ? DateTime.MinValue : Convert.ToDateTime(vo.BreakWaterOut, CultureInfo.InvariantCulture);
                vesselcall.PortLimitIn = string.IsNullOrEmpty(vo.PortLimitIn) ? DateTime.MinValue : Convert.ToDateTime(vo.PortLimitIn, CultureInfo.InvariantCulture);
                vesselcall.PortLimitOut = string.IsNullOrEmpty(vo.PortLimitOut) ? DateTime.MinValue : Convert.ToDateTime(vo.PortLimitOut, CultureInfo.InvariantCulture);
                vesselcall.RecordStatus = vo.RecordStatus;
                vesselcall.CreatedBy = vo.CreatedBy;
                vesselcall.CreatedDate = vo.CreatedDate;
                vesselcall.ModifiedBy = vo.ModifiedBy;
                vesselcall.ModifiedDate = vo.ModifiedDate;
                vesselcall.VCN = vo.VCN;

                if (vo.VesselCallAnchorages != null)
                {
                    ArrivalNotification obj = new ArrivalNotification();
                    obj.VCN = vo.VCN;
                    ICollection<VesselCallAnchorage> vcalanchor = new List<VesselCallAnchorage>();

                    foreach (VesselCallAnchorageVO vcvo in vo.VesselCallAnchorages)
                    {
                        VesselCallAnchorage v = new VesselCallAnchorage();

                        if (!string.IsNullOrWhiteSpace(vcvo.AnchorDropTime))
                        {
                            v = vcvo.MapToEntity();
                            vcalanchor.Add(v);
                        }
                    }

                    obj.VesselCallAnchorages = vcalanchor;
                    vesselcall.ArrivalNotification = obj;
                }
            }
            return vesselcall;
        }

        public static VesselCallVO MapToDto(this VesselCall data)
        {
            VesselCallVO vo = new VesselCallVO();
            if (data != null)
            {
                vo.VesselCallID = data.VesselCallID;
                vo.RecentAgentID = data.RecentAgentID;
                vo.ETA = data.ETA;
                vo.ETD = data.ETD;
                vo.ATA = Convert.ToString(data.ATA, CultureInfo.InvariantCulture);
                vo.ATD = Convert.ToString(data.ATD, CultureInfo.InvariantCulture);
                vo.ATB = data.ATB != null ? data.ATB : null;
                vo.ATUB = data.ATUB != null ? data.ATUB : null;
                vo.BreakWaterIn = data.BreakWaterIn != null ? data.BreakWaterIn.ToString() : null;
                vo.BreakWaterOut = data.BreakWaterOut != null ? data.BreakWaterOut.ToString() : null;
                vo.PortLimitIn = data.PortLimitIn != null ? data.PortLimitIn.ToString() : null;
                vo.PortLimitOut = data.PortLimitOut != null ? data.PortLimitOut.ToString() : null;
                vo.RecordStatus = data.RecordStatus;
                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = vo.ModifiedDate;
                vo.VCN = data.VCN;
                vo.ArrivalNotification = data.ArrivalNotification != null ? data.ArrivalNotification.MapToDto() : null;
                vo.Vessel = data.ArrivalNotification != null && data.ArrivalNotification.Vessel != null ? data.ArrivalNotification.Vessel.MapToDto() : null;
                var vesselcall = data.ArrivalNotification != null && data.ArrivalNotification.VesselCallAnchorages != null ? data.ArrivalNotification.VesselCallAnchorages.Where(a => a.RecordStatus == "A").ToList() : null;
                vo.VesselCallAnchorages = vesselcall != null ? vesselcall.MapToDTO() : null;

                if (!string.IsNullOrWhiteSpace(vo.ATD))
                {                    
                    vo.VesselStatus = "Sailed";
                }
                else
                    if (!string.IsNullOrWhiteSpace(vo.ATA))
                    {
                        if (vo.VesselCallAnchorages != null && vo.VesselCallAnchorages.Count > 0)
                        {
                            var vesselAnchorages = vo.VesselCallAnchorages.LastOrDefault();

                            if (!string.IsNullOrWhiteSpace(vesselAnchorages.AnchorAweighTime))
                            {
                                vo.VesselStatus = "Arrived";
                            }
                            else if (!string.IsNullOrWhiteSpace(vesselAnchorages.AnchorDropTime))
                            {
                                vo.VesselStatus = "Anchored";
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(vo.ATD))
                        {
                            vo.VesselStatus = "Arrived";
                        }
                    }                   
                    else
                    {

                        vo.VesselStatus = "New";
                    }                   
            }
            return vo;
        }

        public static List<VesselCall> MapToEntity(this List<VesselCallVO> vos)
        {
            List<VesselCall> VesselCallEntities = new List<VesselCall>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    VesselCallEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselCallEntities;
        }

        public static List<VesselCallVO> MapToDto(this List<VesselCall> data)
        {
            List<VesselCallVO> vos = new List<VesselCallVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDto());
                }
            }
            return vos;
        }

        public static VesselCallVO MapToDtoForServiceRquest(this VesselCall data)
        {
            VesselCallVO vo = new VesselCallVO();
            if (data != null)
            {
                vo.ETA = data.ETA;
                vo.ETD = data.ETD;
            }
            return vo;
        }
        public static List<VesselCallVO> MapToDtoForServiceRquest(this List<VesselCall> data)
        {
            List<VesselCallVO> vos = new List<VesselCallVO>();
            if (data != null)
            {
                foreach (var vesselcall in data)
                {
                    vos.Add(vesselcall.MapToDtoForServiceRquest());
                }
            }
            return vos;
        }

        public static VcnCloseVO MapToDto(this VcnCloseVO data)
        {
            VcnCloseVO vo = new VcnCloseVO();
            if (data != null)
            {
                vo.VesselCallID = data.VesselCallID;
                vo.VCN = data.VCN;
                vo.RecentAgentID = data.RecentAgentID;
                vo.ETA = data.ETA;
                vo.ETD = data.ETD;
                vo.ETB = data.ETB;
                vo.ETUB = data.ETUB;
                vo.ATA = data.ATA != null ? data.ATA : null;
                vo.ATD = data.ATD != null ? data.ATD : null;
                vo.ATB = data.ATB != null ? data.ATB : null;
                vo.ATUB = data.ATUB != null ? data.ATUB : null;
                vo.BreakWaterIn = data.BreakWaterIn != null ? data.BreakWaterIn : null;
                vo.BreakWaterOut = data.BreakWaterOut != null ? data.BreakWaterOut : null;
                vo.PortLimitIn = data.PortLimitIn != null ? data.PortLimitIn : null;
                vo.PortLimitOut = data.PortLimitOut != null ? data.PortLimitOut : null;
                vo.AnchorUp = data.AnchorUp;
                vo.AnchorDown = data.AnchorDown;
                vo.FromPositionPortCode = data.FromPositionPortCode;
                vo.FromPositionQuayCode = data.FromPositionQuayCode;
                vo.FromPositionBerthCode = data.FromPositionBerthCode;
                vo.FromPositionBollardCode = data.FromPositionBollardCode;
                vo.ToPositionPortCode = data.ToPositionPortCode;
                vo.ToPositionQuayCode = data.ToPositionQuayCode;
                vo.ToPositionBerthCode = data.ToPositionBerthCode;
                vo.ToPositionBollardCode = data.ToPositionBollardCode;
                vo.RecordStatus = data.RecordStatus;
                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = vo.ModifiedDate;

                if (vo.ATD.HasValue)
                    vo.VesselStatus = "Sailed";
            }
            return vo;
        }
    }
}
