using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class VesselCallMovementMapExtenstion
    {
        public static List<VesselCallMovementVO> MapToDTO(this List<VesselCallMovement> data)
        {
            List<VesselCallMovementVO> vos = new List<VesselCallMovementVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDto());
                }
            }
            return vos;
        }

        public static VesselCallMovementVO MapToDto(this VesselCallMovement data)
        {
            VesselCallMovementVO VO = new VesselCallMovementVO();
            if (data != null)
            {
                VO.VesselCallMovementID = data.VesselCallMovementID;
                VO.VCN = data.VCN;
                VO.ServiceRequestID = data.ServiceRequestID ?? default(int);
                VO.FromPositionPortCode = data.FromPositionPortCode;
                VO.FromPositionQuayCode = data.FromPositionQuayCode;
                VO.FromPositionBerthCode = data.FromPositionBerthCode;
                VO.FromPositionBollardCode = data.FromPositionBollardCode;
                VO.ToPositionPortCode = data.ToPositionPortCode;
                VO.ToPositionQuayCode = data.ToPositionQuayCode;
                VO.ToPositionBerthCode = data.ToPositionBerthCode;
                VO.ToPositionBollardCode = data.ToPositionBollardCode;
                VO.SlotStatus = data.SlotStatus;
                VO.SlotDate = data.SlotDate;
                VO.Slot = data.Slot;
                VO.MovementStatus = data.MovementStatus;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;

                VO.ETB = data.ETB;
                VO.ETUB = data.ETUB;
                VO.ATB = data.ATB;
                VO.ATUB = data.ATUB;


                if (data.ArrivalNotification != null)
                {
                    VO.LengthOverAll = data.ArrivalNotification.Vessel.LengthOverallInM ?? default(decimal);

                    VO.GrossRegisteredTonnage = data.ArrivalNotification.Vessel.LengthOverallInM ?? default(long);

                    VO.VesselType = data.ArrivalNotification != null ? data.ArrivalNotification.Vessel != null ? (data.ArrivalNotification.Vessel.SubCategory3 != null ? data.ArrivalNotification.Vessel.SubCategory3.SubCatName : null) : null : null;
                }
                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.VesselCalls.Count > 0)
                    {
                        VO.ETA = data.ArrivalNotification != null ? data.ArrivalNotification.VesselCalls.FirstOrDefault().ETA != null ? data.ArrivalNotification.VesselCalls.FirstOrDefault().ETA : DateTime.MinValue : DateTime.MinValue;
                    }
                    VO.PortCode = data.ArrivalNotification != null ? (data.ArrivalNotification.PortCode != null ? data.ArrivalNotification.PortCode : null) : null;
                }
                if (data.ServiceRequest != null)
                {
                    VO.MovementType = data.ServiceRequest != null ? (data.ServiceRequest.SubCategory1 != null ? data.ServiceRequest.SubCategory1.SubCatName : null) : null;
                    VO.ServiceRequest = data.ServiceRequest != null ? (data.ServiceRequest.SubCategory1 != null ? data.ServiceRequest.SubCategory1.SubCatName : null) : null;
                }

            }

            return VO;
        }

        public static VesselCallMovement MapToEntity(this VesselCallMovementVO vo)
        {
            VesselCallMovement vesselCall = new VesselCallMovement();
            if (vo != null)
            {
                vesselCall.VesselCallMovementID = vo.VesselCallMovementID;
                vesselCall.FromPositionPortCode = vo.FromPositionPortCode;
                vesselCall.FromPositionQuayCode = vo.FromPositionQuayCode;
                vesselCall.FromPositionBerthCode = vo.FromPositionBerthCode;
                vesselCall.ToPositionPortCode = vo.ToPositionPortCode;
                vesselCall.ToPositionBerthCode = vo.ToPositionBerthCode;
                vesselCall.ToPositionBollardCode = vo.ToPositionBollardCode;
                vesselCall.SlotStatus = vo.SlotStatus;
                vesselCall.SlotDate = vo.SlotDate;
                vesselCall.Slot = vo.Slot;
                vesselCall.MovementStatus = vo.MovementStatus;
                vesselCall.RecordStatus = vo.RecordStatus;
                vesselCall.CreatedBy = vo.CreatedBy;
                vesselCall.CreatedDate = vo.CreatedDate;
                vesselCall.ModifiedBy = vo.ModifiedBy;
                vesselCall.ModifiedDate = vo.ModifiedDate;
            }
            return vesselCall;
        }

        public static List<VesselCallMovement> MapToEntity(this List<VesselCallMovementVO> vos)
        {
            List<VesselCallMovement> VesselEntities = new List<VesselCallMovement>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }

    }
}
