using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class VesselArrestDocumentMapExtension
    {

        public static VesselArrestDocument MapToEntity(this VesselArrestDocumentVO vo)
        {
            VesselArrestDocument vesselarrestdocument = new VesselArrestDocument();

            vesselarrestdocument.DocumentID = vo.DocumentID;
            vesselarrestdocument.VAISID = vo.VAISID;
            vesselarrestdocument.CreatedBy = vo.CreatedBy;
            vesselarrestdocument.CreatedDate = vo.CreatedDate;
            vesselarrestdocument.ModifiedBy = vo.ModifiedBy;
            vesselarrestdocument.ModifiedDate = vo.ModifiedDate;

            return vesselarrestdocument;
        }

        public static List<VesselArrestDocument> MapToEntity(this List<VesselArrestDocumentVO> data)
        {
            List<VesselArrestDocument> vos = new List<VesselArrestDocument>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToEntity());
                }
            }
            return vos;
        }

        public static List<VesselArrestDocumentVO> MapToDTO(this List<VesselArrestDocument> vesselarrestdocument)
        {
            List<VesselArrestDocumentVO> vesselArrestDocumentsvo = new List<VesselArrestDocumentVO>();
            if (vesselarrestdocument != null)
            {
                foreach (var vesselarrest in vesselarrestdocument)
                {
                    vesselArrestDocumentsvo.Add(vesselarrest.MapToDto());
                }
            }
            return vesselArrestDocumentsvo;
        }

        public static VesselArrestDocumentVO MapToDto(this VesselArrestDocument data)
        {
            VesselArrestDocumentVO vesselarrestdocumentVo = new VesselArrestDocumentVO();

            if (data != null)
            {
                vesselarrestdocumentVo.DocumentID = data.DocumentID;
                vesselarrestdocumentVo.VAISID = data.VAISID;
                vesselarrestdocumentVo.CreatedBy = data.CreatedBy;
                vesselarrestdocumentVo.CreatedDate = data.CreatedDate;
                vesselarrestdocumentVo.ModifiedBy = data.ModifiedBy;
                vesselarrestdocumentVo.ModifiedDate = data.ModifiedDate;
                vesselarrestdocumentVo.Document = data.Document != null ? data.Document.MapToDTO() : null;
            }
            return vesselarrestdocumentVo;
        }
    }
}
