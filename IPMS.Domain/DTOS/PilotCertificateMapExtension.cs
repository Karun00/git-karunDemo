using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
namespace IPMS.Domain.DTOS
{
    public static class PilotCertificateMapExtension
    {

        public static PilotCertificateVO MapToDTO(this PilotCertificate data)
        {
            PilotCertificateVO vo = new PilotCertificateVO();
            vo.PilotID = data.PilotID;
            vo.PilotCertificateID = data.PilotCertificateID;
            vo.DocumentID = data.DocumentID;
            //vo.CertificateFileName = data.FileName;
            //vo.DocumentName = data.DocumentName;
            vo.RecordStatus = data.RecordStatus;
            return vo;
        }
        public static PilotCertificate MapToDTO(this PilotCertificateVO vo)
        {
            PilotCertificate data = new PilotCertificate();
            data.PilotID = vo.PilotID;
            data.PilotCertificateID = vo.PilotCertificateID;
            data.DocumentID = vo.DocumentID;
            //data.FileName = vo.CertificateFileName;
            //data.DocumentName = vo.DocumentName;
            data.RecordStatus = vo.RecordStatus;
            return data;
        }
    }
}
