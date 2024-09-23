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
    public static class VesselCertificateDetailMapExtension
    {
        public static VesselCertificateDetail MapToEntity(this VesselCertificateDetailVO vo)
        {
            VesselCertificateDetail vesselcert = new VesselCertificateDetail();
            if (vo != null)
            {
                vesselcert.VACERTID = vo.VACERTID;
                vesselcert.VesselID = vo.VesselID;
                vesselcert.CertificateName = vo.CertificateName;
                vesselcert.CertificateNo = vo.CertificateNo;
                vesselcert.DateOfIssue = Convert.ToDateTime(vo.DateOfIssue, CultureInfo.InvariantCulture);
                vesselcert.DateOfValidity = Convert.ToDateTime(vo.DateOfValidity, CultureInfo.InvariantCulture);
                vesselcert.CreatedBy = vo.CreatedBy;
                vesselcert.CreatedDate = vo.CreatedDate;
                vesselcert.ModifiedBy = vo.ModifiedBy;
                vesselcert.ModifiedDate = vo.ModifiedDate;
            }
            return vesselcert;
        }

        public static VesselCertificateDetailVO MapToDto(this VesselCertificateDetail data)
        {
            VesselCertificateDetailVO vo = new VesselCertificateDetailVO();
            if (data != null)
            {
                vo.VACERTID = data.VACERTID;
                vo.VesselID = data.VesselID;
                vo.CertificateName = data.CertificateName;
                vo.CertificateNo = data.CertificateNo;
                if (data.DateOfIssue != null)
                    vo.DateOfIssue = Convert.ToString(data.DateOfIssue, CultureInfo.InvariantCulture);
                if (data.DateOfValidity != null)
                    vo.DateOfValidity = Convert.ToString(data.DateOfValidity, CultureInfo.InvariantCulture);

                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = data.ModifiedDate;
            }
            return vo;
        }
        public static List<VesselCertificateDetail> MapToEntity(this List<VesselCertificateDetailVO> vos)
        {
            List<VesselCertificateDetail> VesselCertEntities = new List<VesselCertificateDetail>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    if (!string.IsNullOrWhiteSpace(vesselvo.CertificateName))
                        VesselCertEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselCertEntities;
        }
        public static List<VesselCertificateDetailVO> MapToDTO(this List<VesselCertificateDetail> data)
        {
            List<VesselCertificateDetailVO> vos = new List<VesselCertificateDetailVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDto());
                }
            }
            return vos;
        }

    }
}
