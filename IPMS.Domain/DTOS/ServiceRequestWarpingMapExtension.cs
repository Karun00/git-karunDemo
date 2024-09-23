using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceRequestWarpingMapExtension
    {
        public static List<ServiceRequestWarpingVO> MapToDto(this List<ServiceRequestWarping> serviceRequestWarpings)
        {
            List<ServiceRequestWarpingVO> serviceRequestVos = new List<ServiceRequestWarpingVO>();
            if (serviceRequestWarpings != null)
            {
                foreach (var serviceRequest in serviceRequestWarpings)
                {
                    serviceRequestVos.Add(serviceRequest.MapToDto());
                }
            }
            return serviceRequestVos;
        }
        public static ServiceRequestWarpingVO MapToDto(this ServiceRequestWarping data)
        {
            ServiceRequestWarpingVO servicerequestVo = new ServiceRequestWarpingVO();
            if (data != null)
            {
                servicerequestVo.ServiceRequestWarpingID = data.ServiceRequestWarpingID;
                servicerequestVo.ServiceRequestID = data.ServiceRequestID;
                servicerequestVo.Warp = data.Warp;
                servicerequestVo.WarpDistance = data.WarpDistance;

                servicerequestVo.FromBollardKey = data.FromPositionPortCode + '.' + data.FromPositionQuayCode + '.' + data.FromPositionBerthCode + '.' + data.FromPositionBollardCode;
                servicerequestVo.ToBollardKey = data.ToPositionPortCode + '.' + data.ToPositionQuayCode + '.' + data.ToPositionBerthCode + '.' + data.ToPositionBollardCode;
                servicerequestVo.FromPositionPortCode = data.FromPositionPortCode;
                servicerequestVo.FromPositionQuayCode = data.FromPositionQuayCode;
                servicerequestVo.FromPositionBerthCode = data.FromPositionBerthCode;
                servicerequestVo.FromPositionBollardCode = data.FromPositionBollardCode;
                servicerequestVo.ToPositionPortCode = data.ToPositionPortCode;
                servicerequestVo.ToPositionQuayCode = data.ToPositionQuayCode;
                servicerequestVo.ToPositionBerthCode = data.ToPositionBerthCode;
                servicerequestVo.ToPositionBollardCode = data.ToPositionBollardCode;

                servicerequestVo.RecordStatus = data.RecordStatus;
                servicerequestVo.CreatedBy = data.CreatedBy;
                servicerequestVo.CreatedDate = data.CreatedDate;
                servicerequestVo.ModifiedBy = data.ModifiedBy;
                servicerequestVo.ModifiedDate = data.ModifiedDate;
            }
            return servicerequestVo;
        }
        public static ServiceRequestWarping MapToEntity(this ServiceRequestWarpingVO vo)
        {

            ServiceRequestWarping servicerequest = new ServiceRequestWarping();

            //string[] fieldsofFromBollard = vo.FromBollardKey.Split('.');
            //string frompositonportCode = fieldsofFromBollard[0];
            //string frompositonquayCode = fieldsofFromBollard[1];
            //string frompositonberthCode = fieldsofFromBollard[2];
            //string frompositonbollardCode = fieldsofFromBollard[3];



            //string[] fieldsofToBollard = vo.ToBollardKey.Split('.');
            //string topositionportCode = fieldsofToBollard[0];
            //string topositionquayCode = fieldsofToBollard[1];
            //string topositionberthCode = fieldsofToBollard[2];
            //string topositionbollardCode = fieldsofToBollard[3];
            //servicerequest.FromPositionPortCode = frompositonportCode;
            //servicerequest.FromPositionQuayCode = frompositonquayCode;
            //servicerequest.FromPositionBerthCode = frompositonberthCode;
            //servicerequest.FromPositionBollardCode = frompositonbollardCode;
            //servicerequest.ToPositionPortCode = topositionportCode;
            //servicerequest.ToPositionQuayCode = topositionquayCode;
            //servicerequest.ToPositionBerthCode = topositionberthCode;
            //servicerequest.ToPositionBollardCode = topositionbollardCode;
            if (vo != null)
            {
                servicerequest.ServiceRequestWarpingID = vo.ServiceRequestWarpingID;
                servicerequest.ServiceRequestID = vo.ServiceRequestID;
                servicerequest.RecordStatus = vo.RecordStatus;
                servicerequest.CreatedBy = vo.CreatedBy;
                servicerequest.CreatedDate = vo.CreatedDate;
                servicerequest.ModifiedBy = vo.ModifiedBy;
                servicerequest.ModifiedDate = vo.ModifiedDate;

                servicerequest.Warp = vo.Warp;
                servicerequest.WarpDistance = vo.WarpDistance;
            }
            return servicerequest;
        }
    }
}
