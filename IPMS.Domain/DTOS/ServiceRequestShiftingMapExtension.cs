using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceRequestShiftingMapExtension
    {

        public static List<ServiceRequestShiftingVO> MapToDto(this List<ServiceRequestShifting> serviceRequestShiftings)
        {
            List<ServiceRequestShiftingVO> serviceRequestVos = new List<ServiceRequestShiftingVO>();
            if (serviceRequestShiftings != null)
            {
                foreach (var serviceRequest in serviceRequestShiftings)
                {
                    serviceRequestVos.Add(serviceRequest.MapToDto());
                }
            }
            return serviceRequestVos;
        }
        public static ServiceRequestShiftingVO MapToDto(this ServiceRequestShifting data)
        {
            //  ServiceRequestShiftingVO serviceRequestShifts = new ServiceRequestShiftingVO();
            //foreach (var servicerequestVo in data)
            //{
            ServiceRequestShiftingVO serviceRequestShift = new ServiceRequestShiftingVO();
            if (data != null)
            {
                serviceRequestShift.ServiceRequestShiftingID = data.ServiceRequestShiftingID;
                serviceRequestShift.ServiceRequestID = data.ServiceRequestID;
                serviceRequestShift.ToPortCode = data.ToPortCode;
                serviceRequestShift.ToQuayCode = data.ToQuayCode;
                serviceRequestShift.ToBerthCode = data.ToBerthCode;
                serviceRequestShift.FromPositionPortCode = data.FromPositionPortCode;
                serviceRequestShift.FromPositionQuayCode = data.FromPositionQuayCode;
                serviceRequestShift.FromPositionBerthCode = data.FromPositionBerthCode;
                serviceRequestShift.FromPositionBollardCode = data.FromPositionBollardCode;
                //   serviceRequestShift.FromPositionBollard = data.Bollard.MapToDTO();

                serviceRequestShift.ToPositionPortCode = data.ToPositionPortCode;
                serviceRequestShift.ToPositionQuayCode = data.ToPositionQuayCode;
                serviceRequestShift.ToPositionBerthCode = data.ToPositionBerthCode;
                serviceRequestShift.ToPositionBollardCode = data.ToPositionBollardCode;

                //   serviceRequestShift.ToPositionBollard = data.Bollard1.MapToDTO();
                //    serviceRequestShift.Berth = data.Berth.MapToDTO();
                serviceRequestShift.BerthKey = data.ToPortCode + '.' + data.ToQuayCode + '.' + data.ToBerthCode;
                //serviceRequestShift.FromBollardKey = data.FromPositionPortCode + '.' + data.FromPositionQuayCode + '.' + data.FromPositionBerthCode + '.' + data.FromPositionBollardCode;
                //serviceRequestShift.ToBollardKey = data.ToPositionPortCode + '.' + data.ToPositionQuayCode + '.' + data.ToPositionBerthCode + '.' + data.ToPositionBollardCode;
                serviceRequestShift.DraftFWD = data.DraftFWD;
                serviceRequestShift.DraftAFT = data.DraftAFT;
                serviceRequestShift.RecordStatus = data.RecordStatus;
                serviceRequestShift.CreatedBy = data.CreatedBy;
                serviceRequestShift.CreatedDate = data.CreatedDate;
                serviceRequestShift.ModifiedBy = data.ModifiedBy;
                serviceRequestShift.ModifiedDate = data.ModifiedDate;
            }
            return serviceRequestShift;
        }

        public static List<ServiceRequestShifting> MapToEntity(this List<ServiceRequestShiftingVO> serviceRequestShiftings)
        {
            List<ServiceRequestShifting> serviceRequestVos = new List<ServiceRequestShifting>();
            if (serviceRequestShiftings != null)
            {
                foreach (var serviceRequest in serviceRequestShiftings)
                {
                    serviceRequestVos.Add(serviceRequest.MapToEntity());
                }
            }
            return serviceRequestVos;
        }
        public static ServiceRequestShifting MapToEntity(this ServiceRequestShiftingVO serviceRequestShiftings)
        {

            
                string[] fieldsofBerth = serviceRequestShiftings.BerthKey.Split('.');
                string portCode = fieldsofBerth[0];
                string quayCode = fieldsofBerth[1];
                string berthCode = fieldsofBerth[2];

                //string[] fieldsofFromBollard = serviceRequestShiftings.FromBollardKey.Split('.');
                //string frompositonportCode = fieldsofFromBollard[0];
                //string frompositonquayCode = fieldsofFromBollard[1];
                //string frompositonberthCode = fieldsofFromBollard[2];
                //string frompositonbollardCode = fieldsofFromBollard[3];



                //string[] fieldsofToBollard = serviceRequestShiftings.ToBollardKey.Split('.');
                //string topositionportCode = fieldsofToBollard[0];
                //string topositionquayCode = fieldsofToBollard[1];
                //string topositionberthCode = fieldsofToBollard[2];
                //string topositionbollardCode = fieldsofToBollard[3];


                ServiceRequestShifting servicerequestShift = new ServiceRequestShifting();
                if (serviceRequestShiftings != null)
                {
                servicerequestShift.ServiceRequestID = serviceRequestShiftings.ServiceRequestID;
                servicerequestShift.ServiceRequestShiftingID = serviceRequestShiftings.ServiceRequestShiftingID;
                servicerequestShift.ToPortCode = portCode;
                servicerequestShift.ToQuayCode = quayCode;
                servicerequestShift.ToBerthCode = berthCode;

                //servicerequestShift.FromPositionPortCode = frompositonportCode;
                //servicerequestShift.FromPositionQuayCode = frompositonquayCode;
                //servicerequestShift.FromPositionBerthCode = frompositonberthCode;
                //servicerequestShift.FromPositionBollardCode = frompositonbollardCode;
                //servicerequestShift.ToPositionPortCode = topositionportCode;
                //servicerequestShift.ToPositionQuayCode = topositionquayCode;
                //servicerequestShift.ToPositionBerthCode = topositionberthCode;
                //servicerequestShift.ToPositionBollardCode = topositionbollardCode;
                servicerequestShift.DraftFWD = serviceRequestShiftings.DraftFWD;
                servicerequestShift.DraftAFT = serviceRequestShiftings.DraftAFT;
                servicerequestShift.RecordStatus = serviceRequestShiftings.RecordStatus;
                servicerequestShift.CreatedBy = serviceRequestShiftings.CreatedBy;
                servicerequestShift.CreatedDate = serviceRequestShiftings.CreatedDate;
                servicerequestShift.ModifiedBy = serviceRequestShiftings.ModifiedBy;
                servicerequestShift.ModifiedDate = serviceRequestShiftings.ModifiedDate;
            }
            return servicerequestShift;

        }
      

    }
}
