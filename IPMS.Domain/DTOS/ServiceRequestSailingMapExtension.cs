using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceRequestSailingMapExtension
    {
        public static List<ServiceRequestSailingVO> MapToDto(this List<ServiceRequestSailing> serviceRequestSailings)
        {
            List<ServiceRequestSailingVO> serviceRequestVos = new List<ServiceRequestSailingVO>();
            if (serviceRequestSailings != null)
            {
                foreach (var serviceRequest in serviceRequestSailings)
                {
                    serviceRequestVos.Add(serviceRequest.MapToDto());
                }
            }
            return serviceRequestVos;
        }
        public static ServiceRequestSailingVO MapToDto(this ServiceRequestSailing data)
        {
            ServiceRequestSailingVO servicerequestVo = new ServiceRequestSailingVO();
            if (data != null)
            {
                servicerequestVo.ServiceRequestSailingID = data.ServiceRequestSailingID;
                servicerequestVo.ServiceRequestID = data.ServiceRequestID;
                servicerequestVo.MarineRevenueCleared = data.MarineRevenueCleared == "Y" ? true : false;
                servicerequestVo.DocumentID = data.DocumentID;
                servicerequestVo.RecordStatus = data.RecordStatus;
                servicerequestVo.CreatedBy = data.CreatedBy;
                servicerequestVo.CreatedDate = data.CreatedDate;
                servicerequestVo.ModifiedBy = data.ModifiedBy;
                servicerequestVo.ModifiedDate = data.ModifiedDate;
                if (data.Document != null)
                {
                    servicerequestVo.ServiceRequestDocument = data.Document != null ? data.Document.MapToDTO() : null;
                }
            }
            return servicerequestVo;
        }
        public static ServiceRequestSailing MapToEntity(this ServiceRequestSailingVO vo)
        {
            ServiceRequestSailing servicerequest = new ServiceRequestSailing();
            if (vo != null)
            {
                servicerequest.ServiceRequestSailingID = vo.ServiceRequestSailingID;
                servicerequest.ServiceRequestID = vo.ServiceRequestID;
                servicerequest.MarineRevenueCleared = vo.MarineRevenueCleared.ToString();
                servicerequest.DocumentID = vo.DocumentID;
                servicerequest.RecordStatus = vo.RecordStatus;
                servicerequest.CreatedBy = vo.CreatedBy;
                servicerequest.CreatedDate = vo.CreatedDate;
                servicerequest.ModifiedBy = vo.ModifiedBy;
                servicerequest.ModifiedDate = vo.ModifiedDate;
                if (vo.ServiceRequestDocument != null)
                {
                    servicerequest.Document = vo.ServiceRequestDocument.MapToEntity();
                }
            }
            return servicerequest;
        }

        //public static List<ServiceRequestShiftingVO> MapToDTO(this List<ServiceRequestShifting> serviceRequestShiftings)
        //{
        //    List<ServiceRequestShiftingVO> serviceRequestVos = new List<ServiceRequestShiftingVO>();
        //    foreach (var serviceRequest in serviceRequestShiftings)
        //    {
        //        serviceRequestVos.Add(serviceRequest.MapToDTO());
        //    }
        //    return serviceRequestVos;
        //}
        //public static ServiceRequestShiftingVO MapToDTO(this ServiceRequestShifting data)
        //{
        //    //  ServiceRequestShiftingVO serviceRequestShifts = new ServiceRequestShiftingVO();
        //    //foreach (var servicerequestVo in data)
        //    //{
        //    ServiceRequestShiftingVO serviceRequestShift = new ServiceRequestShiftingVO();
        //    serviceRequestShift.ServiceRequestShiftingID = data.ServiceRequestShiftingID;
        //    serviceRequestShift.ServiceRequestID = data.ServiceRequestID;
        //    serviceRequestShift.ToPortCode = data.ToPortCode;
        //    serviceRequestShift.ToQuayCode = data.ToQuayCode;
        //    serviceRequestShift.ToBerthCode = data.ToBerthCode;
        //    serviceRequestShift.FromPositionPortCode = data.FromPositionPortCode;
        //    serviceRequestShift.FromPositionQuayCode = data.FromPositionQuayCode;
        //    serviceRequestShift.FromPositionBerthCode = data.FromPositionBerthCode;
        //    serviceRequestShift.FromPositionBollardCode = data.FromPositionBollardCode;
        //    serviceRequestShift.ToPositionPortCode = data.ToPositionPortCode;
        //    serviceRequestShift.ToPositionQuayCode = data.ToPositionQuayCode;
        //    serviceRequestShift.ToPositionBerthCode = data.ToPositionBerthCode;
        //    serviceRequestShift.ToPositionBollardCode = data.ToPositionBollardCode;
        //    serviceRequestShift.DraftFWD = data.DraftFWD;
        //    serviceRequestShift.DraftAFT = data.DraftAFT;
        //    serviceRequestShift.RecordStatus = data.RecordStatus;
        //    serviceRequestShift.CreatedBy = data.CreatedBy;
        //    serviceRequestShift.CreatedDate = data.CreatedDate;
        //    serviceRequestShift.ModifiedBy = data.ModifiedBy;
        //    serviceRequestShift.ModifiedDate = data.ModifiedDate;

        //    //  serviceRequestShifts.Add(serviceRequestShift);
        //    // }

        //    return serviceRequestShift;
        //}
        //public static List<ServiceRequestShifting> MapToEntity(this List<ServiceRequestShiftingVO> serviceRequestShiftings)
        //{
        //    List<ServiceRequestShifting> serviceRequestVos = new List<ServiceRequestShifting>();
        //    foreach (var serviceRequest in serviceRequestShiftings)
        //    {
        //        serviceRequestVos.Add(serviceRequest.MapToEntity());
        //    }
        //    return serviceRequestVos;
        //}
        //public static ServiceRequestShifting MapToEntity(this ServiceRequestShiftingVO serviceRequestShiftings)
        //{


        //    //    ServiceRequestShifting servicerequestBerths = new ServiceRequestShifting;
        //    //   foreach (var serviceRequestShift in serviceRequestShiftings)
        //    // //   {
        //    //     if (serviceRequestShift.BerthKey.Length > 0)
        //    //    {

        //    string[] fieldsofBerth = serviceRequestShiftings.BerthKey.Split('.');
        //    string portCode = fieldsofBerth[0];
        //    string quayCode = fieldsofBerth[1];
        //    string berthCode = fieldsofBerth[2];

        //    string[] fieldsofFromBollard = serviceRequestShiftings.FromBollardKey.Split('.');
        //    string frompositonportCode = fieldsofFromBollard[0];
        //    string frompositonquayCode = fieldsofFromBollard[1];
        //    string frompositonberthCode = fieldsofFromBollard[2];
        //    string frompositonbollardCode = fieldsofFromBollard[3];



        //    string[] fieldsofToBollard = serviceRequestShiftings.ToBollardKey.Split('.');
        //    string topositionportCode = fieldsofToBollard[0];
        //    string topositionquayCode = fieldsofToBollard[1];
        //    string topositionberthCode = fieldsofToBollard[2];
        //    string topositionbollardCode = fieldsofToBollard[3];


        //    ServiceRequestShifting servicerequestShift = new ServiceRequestShifting();
        //    servicerequestShift.ServiceRequestID = serviceRequestShiftings.ServiceRequestID;
        //    servicerequestShift.ToPortCode = portCode;
        //    servicerequestShift.ToQuayCode = quayCode;
        //    servicerequestShift.ToBerthCode = berthCode;

        //    servicerequestShift.FromPositionPortCode = frompositonportCode;
        //    servicerequestShift.FromPositionQuayCode = frompositonquayCode;
        //    servicerequestShift.FromPositionBerthCode = frompositonberthCode;
        //    servicerequestShift.FromPositionBollardCode = frompositonbollardCode;
        //    servicerequestShift.ToPositionPortCode = topositionportCode;
        //    servicerequestShift.ToPositionQuayCode = topositionquayCode;
        //    servicerequestShift.ToPositionBerthCode = topositionberthCode;
        //    servicerequestShift.ToPositionBollardCode = topositionbollardCode;
        //    servicerequestShift.DraftFWD = serviceRequestShiftings.DraftFWD;
        //    servicerequestShift.DraftAFT = serviceRequestShiftings.DraftAFT;
        //    servicerequestShift.RecordStatus = serviceRequestShiftings.RecordStatus;
        //    servicerequestShift.CreatedBy = serviceRequestShiftings.CreatedBy;
        //    servicerequestShift.CreatedDate = serviceRequestShiftings.CreatedDate;
        //    servicerequestShift.ModifiedBy = serviceRequestShiftings.ModifiedBy;
        //    servicerequestShift.ModifiedDate = serviceRequestShiftings.ModifiedDate;

        //    //   servicerequestBerths.Add(servicerequestShift);
        //    //   }
        //    //  }

        //    return servicerequestShift;
        //    //return berthKeyArray;
        //}

    }
}
