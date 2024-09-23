using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class VesselSAMSAStopDocumentMapExtension
    {
        public static VesselSAMSAStopDocument MapToEntity(this VesselSAMSAStopDocumentVO vo)
        {
            VesselSAMSAStopDocument vesselsamsastopdocument = new VesselSAMSAStopDocument();

            vesselsamsastopdocument.DocumentID = vo.DocumentID;
            vesselsamsastopdocument.VAISID = vo.VAISID;
            vesselsamsastopdocument.CreatedBy = vo.CreatedBy;
            vesselsamsastopdocument.CreatedDate = vo.CreatedDate;
            vesselsamsastopdocument.ModifiedBy = vo.ModifiedBy;
            vesselsamsastopdocument.ModifiedDate = vo.ModifiedDate;

            return vesselsamsastopdocument;
        }

        public static List<VesselSAMSAStopDocument> MapToEntity(this List<VesselSAMSAStopDocumentVO> data)
        {
            List<VesselSAMSAStopDocument> vos = new List<VesselSAMSAStopDocument>();

            foreach (var vessel in data)
            {
                vos.Add(vessel.MapToEntity());
            }

            return vos;
        }

        public static List<VesselSAMSAStopDocumentVO> MapToDTO(this List<VesselSAMSAStopDocument> vesselarrestdocument)
        {
            List<VesselSAMSAStopDocumentVO> vesselarrestdocumentsvo = new List<VesselSAMSAStopDocumentVO>();

            foreach (var vesselarrest in vesselarrestdocument)
            {
                vesselarrestdocumentsvo.Add(vesselarrest.MapToDTO());
            }

            return vesselarrestdocumentsvo;
        }

        public static VesselSAMSAStopDocumentVO MapToDTO(this VesselSAMSAStopDocument data)
        {
            VesselSAMSAStopDocumentVO vesselarrestdocumentVo = new VesselSAMSAStopDocumentVO();

            vesselarrestdocumentVo.DocumentID = data.DocumentID;
            vesselarrestdocumentVo.VAISID = data.VAISID;
            vesselarrestdocumentVo.CreatedBy = data.CreatedBy;
            vesselarrestdocumentVo.CreatedDate = data.CreatedDate;
            vesselarrestdocumentVo.ModifiedBy = data.ModifiedBy;
            vesselarrestdocumentVo.ModifiedDate = data.ModifiedDate;
            vesselarrestdocumentVo.Document = data.Document != null ? data.Document.MapToDTO() : null;

            return vesselarrestdocumentVo;
        }
    }
}
