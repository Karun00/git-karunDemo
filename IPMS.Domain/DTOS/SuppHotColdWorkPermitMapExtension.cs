using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class SuppHotColdWorkPermitMapExtension
    {

        public static List<SuppHotColdWorkPermit> MapToEntity(this List<SuppHotColdWorkPermitVO> suppHotColdWorkPermitsVO)
        {
            List<SuppHotColdWorkPermit> lstsuppHotColdWorkPermit = new List<SuppHotColdWorkPermit>();

            foreach (SuppHotColdWorkPermitVO suppHotColdWorkPermitVO in suppHotColdWorkPermitsVO)
            {
                lstsuppHotColdWorkPermit.Add(suppHotColdWorkPermitVO.MapToEntity());
            }


            return lstsuppHotColdWorkPermit;
        }

        public static SuppHotColdWorkPermitVO MapToDTO(this SuppHotColdWorkPermit data)
        {
            SuppHotColdWorkPermitVO suppHotColdWorkPermitVO = new SuppHotColdWorkPermitVO();

            suppHotColdWorkPermitVO.SuppHotColdWorkPermitID = data.SuppHotColdWorkPermitID;
            suppHotColdWorkPermitVO.SuppServiceRequestID = data.SuppServiceRequestID;
            suppHotColdWorkPermitVO.GassFreeCertificateAvailable = data.GassFreeCertificateAvailable;
            suppHotColdWorkPermitVO.GassFreeCertificateValidity = data.GassFreeCertificateValidity != null ? Convert.ToString(data.GassFreeCertificateValidity, CultureInfo.InvariantCulture) : null;
            suppHotColdWorkPermitVO.GassFreeIssuingAuthority = data.GassFreeIssuingAuthority;
            suppHotColdWorkPermitVO.LocationID = data.LocationID;
            suppHotColdWorkPermitVO.CreatedBy = data.CreatedBy;
            suppHotColdWorkPermitVO.CreatedDate = data.CreatedDate;
            suppHotColdWorkPermitVO.ModifiedBy = data.ModifiedBy;
            suppHotColdWorkPermitVO.ModifiedDate = data.ModifiedDate;
            suppHotColdWorkPermitVO.LocationName = data.Location.LocationName;
            suppHotColdWorkPermitVO.OtherLocation = data.OtherLocation;

            if (data.SuppHotColdWorkPermitDocuments.Count > 0)
            {
                List<SuppHotColdWorkPermitDocumentVO> lstSuppHotColdWorkPermitDocumentVO = new List<SuppHotColdWorkPermitDocumentVO>();
                foreach (SuppHotColdWorkPermitDocument suppHotColdWorkPermitDocument in data.SuppHotColdWorkPermitDocuments)
                {
                    lstSuppHotColdWorkPermitDocumentVO.Add(suppHotColdWorkPermitDocument.MapToDTO());
                }

                suppHotColdWorkPermitVO.SuppHotColdWorkPermitDocumentsVO = lstSuppHotColdWorkPermitDocumentVO;
            }


            return suppHotColdWorkPermitVO;
        }

        public static SuppHotColdWorkPermit MapToEntity(this SuppHotColdWorkPermitVO vo)
        {
            SuppHotColdWorkPermit suppHotColdWorkPermit = new SuppHotColdWorkPermit();

            suppHotColdWorkPermit.SuppHotColdWorkPermitID = vo.SuppHotColdWorkPermitID;
            suppHotColdWorkPermit.SuppServiceRequestID = vo.SuppServiceRequestID;
            suppHotColdWorkPermit.GassFreeCertificateAvailable = vo.GassFreeCertificateAvailable;
            if (vo.GassFreeCertificateValidity != "" && vo.GassFreeCertificateValidity != null)
            {
                suppHotColdWorkPermit.GassFreeCertificateValidity = DateTime.Parse(vo.GassFreeCertificateValidity, CultureInfo.InvariantCulture);
            }
            suppHotColdWorkPermit.GassFreeIssuingAuthority = vo.GassFreeIssuingAuthority;
            suppHotColdWorkPermit.LocationID = vo.LocationID;
            suppHotColdWorkPermit.CreatedBy = vo.CreatedBy;
            suppHotColdWorkPermit.CreatedDate = vo.CreatedDate;
            suppHotColdWorkPermit.ModifiedBy = vo.ModifiedBy;
            suppHotColdWorkPermit.ModifiedDate = vo.ModifiedDate;
            suppHotColdWorkPermit.OtherLocation = vo.OtherLocation;

            return suppHotColdWorkPermit;
        }
    }
}
